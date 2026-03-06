using SGRHDevOps.Core.Domain.Entities.ReservationModule;
using SGRHDevOps.Core.Domain.Interfaces.ReservationModule;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.ReservationModule
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(SGRHContext context) : base(context) { }
    }
}
