namespace College.Models
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title {  get; set; }
        public required int Hours { get; set; }
        public Department? Department { get; set; }  //Reference navigation property
        public ICollection<Student> Students { get; set;} = [];
    }
}
