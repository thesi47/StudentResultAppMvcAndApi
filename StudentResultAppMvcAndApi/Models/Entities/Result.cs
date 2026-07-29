namespace StudentResultAppMvcAndApi.Models.Entities
{
    public class Result
    {
        public string ResultId { get; set; }
        public int PhysicsMark {  get; set; }
        public int ChemistryMark { get; set; }
        public int MathMark { get; set; }
        public string StudentId { get; set; } 
        public Student Student { get; set; }
    }
}
