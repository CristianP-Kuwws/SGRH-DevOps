using SGRHDevOps.Core.Domain.Entities.ReservationModule;
using SGRHDevOps.Core.Domain.Interfaces.ReservationModule;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.ReservationModule
{
    public class ReservationServiceRepository : GenericRepository<ReservationService>, IReservationServiceRepository
    {
        public ReservationServiceRepository(SGRHContext context) : base(context) { }
    }
}
