namespace College.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string First_Name { get; set; }
        public required string Last_Name { get; set;}
        public required int Semester { get; set; }
        public Department? Department { get; set; } //Reference navigation property
        public ICollection<Course> Courses { get; set;} = [];
    }
}
