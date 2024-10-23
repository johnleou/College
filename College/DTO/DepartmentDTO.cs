using System.Text.Json.Serialization;

namespace College.DTO
{
    public record DepartmentDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Years { get; set; }
    }
}
