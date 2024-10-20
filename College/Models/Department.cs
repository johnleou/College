namespace College.Models
{
    public class Department
    {
        public int Id { get; set; } 
        public required string Title { get; set; }
        public required int Years { get; set; }
        public virtual ICollection<Student> Students { get; set; } = [];
        public virtual ICollection<Course> Courses { get; set; } = [];
    }
}
