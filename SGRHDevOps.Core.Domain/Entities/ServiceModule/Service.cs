using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Domain.Entities.ServiceModule
{
    public class Service : AuditEntity
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
