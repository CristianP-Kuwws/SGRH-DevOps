using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;
using SGRHDevOps.Core.Domain.Interfaces.ReservationModule;
using SGRHDevOps.Core.Domain.Interfaces.ServiceModule;
using SGRHDevOps.Core.Domain.Interfaces.UserManagement;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Hotel;
using SGRHDevOps.Infrastructure.Persistence.Repositories.ReservationModule;
using SGRHDevOps.Infrastructure.Persistence.Repositories.ServiceModule;
using SGRHDevOps.Infrastructure.Persistence.Repositories.UserManagement;

namespace SGRHDevOps.Infrastructure.Persistence.IOC
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayerIOC(this IServiceCollection services, IConfiguration config)
        {
            #region Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SGRHContext>(options =>
                {
                    options.UseInMemoryDatabase("LibraryMSInMemoryDb");
                });
            }
            else
            {
                throw new InvalidOperationException("La configuracion actual solo admite UseInMemoryDatabase=true para este entorno.");
            }
            #endregion


            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationServiceRepository, ReservationServiceRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            #endregion
              

        }
    }
}
