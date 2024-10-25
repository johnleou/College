using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record CourseDetailDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Hours { get; set; }
        public DepartmentDTO? Department { get; set; }
        public ICollection<StudentDTO>? Students { get; set; }
    }
}
