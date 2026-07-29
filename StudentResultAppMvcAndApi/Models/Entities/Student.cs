namespace StudentResultAppMvcAndApi.Models.Entities
{
    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string PhoneNumber { get; set; }
        public int StudentAge { get; set; }
        public int TotalMarks { get; set; }
        public double AverageMark { get; set; }
        public string LtrGrade { get; set; }
        public double Gpa { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        public Result result { get; set; }
    }
}
