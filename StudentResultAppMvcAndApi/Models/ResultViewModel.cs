using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentResultAppMvcAndApi.Models
{
    public class ResultViewModel
    {
        public string ResultId { get; set; }

        [Required(ErrorMessage = "Physics marks are required")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
        public int PhysicsMark { get; set; }

        [Required(ErrorMessage = "Chemistry marks are required")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
        public int ChemistryMark { get; set; }

        [Required(ErrorMessage = "Math marks are required")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
        public int MathMark { get; set; }

        public string StudentId { get; set; }
        public IEnumerable<SelectListItem>? Students { get; set; }
    }
}
