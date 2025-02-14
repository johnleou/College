using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.DTO
{
    public record StudentDTO
    {        
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage ="Please give proper First Name")]
        public string First_Name { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Please give proper Last Name")]
        public string Last_Name { get; set; }
        [Required]
        [Range(0, 20, ErrorMessage = "Semester must be between 0-20")]
        public int Semester { get; set; }
    }
}
