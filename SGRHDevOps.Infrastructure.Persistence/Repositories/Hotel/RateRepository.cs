using SGRHDevOps.Core.Domain.Entities.Hotel;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.Hotel
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {
        public RateRepository(SGRHContext context) : base(context) { }
    }
}
