using College.Models;
using System.Reflection.Metadata;

namespace College.DTO
{
    public class CourseDetailDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Hours { get; set; }
        public DepartmentDTO? Department { get; set; }
        public ICollection<StudentDTO>? Students { get; set; }
    }
}
