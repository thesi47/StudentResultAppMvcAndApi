namespace StudentResultAppMvcAndApi.Models.Entities
{
    public class Department
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<Student> students { get; set; } = new List<Student>();
    }
}
