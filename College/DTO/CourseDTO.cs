using College.Models;
using System.Text.Json.Serialization;

namespace College.DTO
{
    public class CourseDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Hours { get; set;}        
    }
}
