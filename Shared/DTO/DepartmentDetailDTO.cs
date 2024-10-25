using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record DepartmentDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Years { get; set; }
        public ICollection<StudentDTO>? Students { get; set; }
        public ICollection<CourseDTO>? Courses { get; set; }
    }
}
