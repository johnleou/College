using System.Text.Json.Serialization;

namespace College.DTO
{
    public record DepartmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Years { get; set; }

        [JsonIgnore]
        public ICollection<StudentDTO>? Students { get; set; }

        [JsonIgnore]
        public ICollection<CourseDTO>? Courses { get; set; }
    }
}
