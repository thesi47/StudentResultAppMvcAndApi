using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentResultAppMvcAndApi.Models
{
    public class StudentViewModel
    {
        public string StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int StudentAge { get; set; }
        public double TotalMarks { get; set; }
        public double AverageMark { get; set; }
        public double Gpa { get; set; }
        public string? LtrGrade { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string DepartmentId { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }
    }
}
