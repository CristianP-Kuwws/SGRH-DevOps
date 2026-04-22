using SGRHDevOps.Core.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace SGRHDevOps.Core.Domain.Entities.Hotel
{
    public class Floor : AuditEntity
    {
        [Key]
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = "active";
    }
}
