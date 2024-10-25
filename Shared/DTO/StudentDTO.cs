using System.Text.Json.Serialization;

namespace Shared.DTO
{
    public record StudentDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Semester { get; set; }
    }
}
