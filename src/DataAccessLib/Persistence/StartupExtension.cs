using DataAccessLib.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLib.Persistence
{
    public static class StartupExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            bool isDetailedLogEnabled = true;
            return services
                .AddDbContext<DesktopEvalDBContext>(options =>
                {
                    if (isDetailedLogEnabled)
                    {
                        options.EnableDetailedErrors()
                        .EnableSensitiveDataLogging();
                    }
                })
                .AddRepositories();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IDesktopEvalRepository), typeof(DesktopEvalRepository));
            return services;
        }
    }
}