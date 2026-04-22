using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Dtos.Hotel
{
    public class FloorDto
    {
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = "active";
    }
}
