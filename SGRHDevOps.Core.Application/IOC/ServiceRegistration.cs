using Microsoft.Extensions.DependencyInjection;
using SGRHDevOps.Core.Application.Interfaces.ReservationModule;
using SGRHDevOps.Core.Application.Services.ReservationModule;
using System.Reflection;

namespace SGRHDevOps.Core.Application.IOC
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayerIOC(this IServiceCollection services)
        {
            #region Configurations
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Services
            services.AddScoped<IReservationService, ReservationService>();
            #endregion
        }
    }
}
