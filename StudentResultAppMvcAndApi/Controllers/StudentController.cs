using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentResultAppMvcAndApi.Data;
using StudentResultAppMvcAndApi.Models;
using StudentResultAppMvcAndApi.Models.Entities;

namespace StudentResultAppMvcAndApi.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task CalculateAndUpdateMarks(string studentId)
        {
            var student = await _context.students.FindAsync(studentId);
            if (student == null)
                return;

            var result = await _context.results
                .FirstOrDefaultAsync(r => r.StudentId == studentId);

            if (result != null)
            {
                student.TotalMarks = result.PhysicsMark + result.ChemistryMark + result.MathMark;

                student.AverageMark = student.TotalMarks / 3;
                if (student.AverageMark > 80)
                {
                    student.LtrGrade = "A+";
                    student.Gpa = 4.00;
                }
                else if (student.AverageMark > 70)
                {
                    student.LtrGrade = "A";
                    student.Gpa = 3.75;
                }
                else if (student.AverageMark > 65)
                {
                    student.LtrGrade = "A-";
                    student.Gpa = 3.50;
                }
                else if (student.AverageMark > 60)
                {
                    student.LtrGrade = "B";
                    student.Gpa = 3.00;
                }
                else if (student.AverageMark > 50)
                {
                    student.LtrGrade = "C";
                    student.Gpa = 2.00;
                }
                else if (student.AverageMark > 40)
                {
                    student.LtrGrade = "D";
                    student.Gpa = 1.00;
                }
                else
                {
                    student.LtrGrade = "F";
                    student.Gpa = 0.00;
                }

                await _context.SaveChangesAsync();
            }
        }
        public async Task<IActionResult> Index(string? searchQuery, string? departmentId, int? age, string? letterGrade)
        {
            var model = new SearchViewModel
            {
                SearchQuery = searchQuery,
                DepartmentId = departmentId,
                Age = age,
                LetterGrade = letterGrade,
                Departments = await _context.departments.Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.DepartmentName
                    }).ToListAsync()
            };

            var query = _context.students
                .Include(s => s.Department)
                .Include(s => s.result)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(s =>
                    s.StudentName.Contains(searchQuery) ||
                    s.StudentId.ToString().Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(departmentId) && departmentId != "")
            {
                query = query.Where(s => s.DepartmentId == departmentId);
            }

            if (age.HasValue && age.Value > 0)
            {
                query = query.Where(s => s.StudentAge == age.Value);
            }

            if (!string.IsNullOrWhiteSpace(letterGrade))
            {
                query = query.Where(s => s.LtrGrade == letterGrade);
            }

            var students = await query
                .OrderBy(s => s.StudentId)
                .ToListAsync();

            // Calculate and update marks for all students
            foreach (var student in students)
            {
                await CalculateAndUpdateMarks(student.StudentId);
            }

            model.Students = students;
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new StudentViewModel
            {
                Departments = await _context.departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.DepartmentName
                    })
                    .ToListAsync()
            };

            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _context.departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.DepartmentName
                    })
                    .ToListAsync();

                return View(model);
            }

            var student = new Student
            {
                StudentName = model.StudentName,
                StudentAge = model.StudentAge,
                DepartmentId = model.DepartmentId
            };

            _context.students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.students.FindAsync(id);

            if (student == null)
                return NotFound();

            var model = new StudentViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                StudentAge = student.StudentAge,
                DepartmentId = student.DepartmentId,

                Departments = await _context.departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.DepartmentName
                    })
                    .ToListAsync()
            };

            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _context.departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.DepartmentName
                    })
                    .ToListAsync();

                return View(model);
            }

            var student = await _context.students.FindAsync(model.StudentId);

            if (student == null)
                return NotFound();

            student.StudentName = model.StudentName;
            student.StudentAge = model.StudentAge;
            student.DepartmentId = model.DepartmentId;

            await _context.SaveChangesAsync();
            await CalculateAndUpdateMarks(model.StudentId);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _context.students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (model == null)
                return NotFound();

            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var model = await _context.students.FindAsync(id);
            if (model == null) return NotFound();

            _context.students.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
