using SGRHDevOps.Core.Domain.Entities.ServiceModule;
using SGRHDevOps.Core.Domain.Interfaces.ServiceModule;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.ServiceModule
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(SGRHContext context) : base(context) { }
    }

}
