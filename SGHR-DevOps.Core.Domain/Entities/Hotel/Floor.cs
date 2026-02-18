using SGHR_DevOps.Common.Base;

namespace SGHR_DevOps.Entities.Hotel
{
    public class Floor : AuditEntity

    {
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = "active";
    }
}
