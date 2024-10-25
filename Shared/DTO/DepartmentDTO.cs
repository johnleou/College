using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record DepartmentDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Years { get; set; }
    }
}
