using SGHR_DevOps.Core.Domain.Common.Base;

namespace SGHR_DevOps.Entities.Core.Domain.ServiceModule
{
    public class Service : AuditEntity
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
