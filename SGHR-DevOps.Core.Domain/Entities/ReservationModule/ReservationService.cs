using SGHR_DevOps.Common.Base;

namespace SGHR_DevOps.Entities.ReservationModule
{
    public class ReservationService : AuditEntity
    {
        public int ReservationServiceId { get; set; }
        public int ReservationId { get; set; }
        public int ServiceCategoryId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
