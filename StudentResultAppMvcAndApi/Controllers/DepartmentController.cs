using Microsoft.AspNetCore.Mvc;
using StudentResultAppMvcAndApi.Data;
using StudentResultAppMvcAndApi.Models.Entities;

namespace StudentResultAppMvcAndApi.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(Student student)
        {
            return View(student);
        }
    }
}
