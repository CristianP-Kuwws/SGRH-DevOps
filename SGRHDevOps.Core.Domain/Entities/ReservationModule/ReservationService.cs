using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Domain.Entities.ReservationModule
{
    public class ReservationService : AuditEntity
    {
        public int ReservationServiceId { get; set; }
        public int ReservationId { get; set; }
        public int ServiceCategoryId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
