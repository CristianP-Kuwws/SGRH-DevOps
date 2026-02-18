using Microsoft.EntityFrameworkCore;
using SGHR_DevOps.Entities.Hotel;
using SGHR_DevOps.Entities.ReservationModule;
using SGHR_DevOps.Entities.ServiceModule;
using System.Reflection;

namespace SGRH_DevOps.Infrastructure.Persistence.Contexts
{
    public class SGRHContext : DbContext
    {
        public SGRHContext(DbContextOptions<SGRHContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationService> ReservationServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
