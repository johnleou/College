using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record CourseDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Hours { get; set; }
    }
}
