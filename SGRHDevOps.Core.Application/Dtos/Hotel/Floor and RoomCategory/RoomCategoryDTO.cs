using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom
{
    public class RoomCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxCapacity { get; set; }
        public string Amenities { get; set; }
    }
}
