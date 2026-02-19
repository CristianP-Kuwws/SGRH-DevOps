using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGRHDevOps.Infrastructure.Persistence.Contexts;

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
                var connectionString = config.GetValue<string>("ConnectionStrings:DefaultConnection");

                services.AddDbContext<SGRHContext>(
                    (serviceProvider, options) =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseNpgsql(
                             connectionString,
                             options => options.MigrationsAssembly(typeof(SGRHContext).Assembly.FullName)
                        );
                    },
                    contextLifetime: ServiceLifetime.Scoped,
                    optionsLifetime: ServiceLifetime.Scoped
                );
            }
            #endregion


            #region Repositories
            #endregion


        }
    }
}
