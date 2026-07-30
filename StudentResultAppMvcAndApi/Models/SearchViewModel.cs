using Microsoft.AspNetCore.Mvc.Rendering;
using StudentResultAppMvcAndApi.Models.Entities;

namespace StudentResultAppMvcAndApi.Models
{
    public class SearchViewModel
    {
        public string? SearchQuery { get; set; }
        public string? DepartmentId { get; set; }
        public int? Age { get; set; }
        public string? LetterGrade { get; set; }
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<SelectListItem>? Departments { get; set; }
    }
}
