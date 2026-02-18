using SGHR_DevOps.Core.Domain.Common.Base;

namespace SGHR_DevOps.Core.Domain.Entities.Hotel
{
    public class Rate : AuditEntity
    {
        public int RateId { get; set; }
        public int CategoryId { get; set; }
        public int SeasonId { get; set; }
        public decimal NightPrice { get; set; }

    }
}
