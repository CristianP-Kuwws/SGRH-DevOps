using SGRHDevOps.Core.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace SGRHDevOps.Core.Domain.Entities.Hotel
{
    public class RoomCategory : AuditEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxCapacity { get; set; }
        public string Amenities { get; set; }
    }
}
