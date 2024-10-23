using System.Reflection.Metadata;

namespace College.DTO
{
    public class StudentDetailDTO
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Semester {  get; set; }
        public DepartmentDTO? Department { get; set; }
        public ICollection<CourseDTO> Courses { get; set; } = [];
    }
}
