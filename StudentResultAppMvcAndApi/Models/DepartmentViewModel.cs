using System.ComponentModel.DataAnnotations;

namespace StudentResultAppMvcAndApi.Models
{
    public class DepartmentViewModel
    {
        public string DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
