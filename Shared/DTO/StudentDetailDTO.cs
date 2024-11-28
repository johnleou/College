using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record StudentDetailDTO
    {        
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Semester { get; set; }
        public DepartmentDTO? Department { get; set; }
        public ICollection<CourseDTO> Courses { get; set; } = [];
    }
}
