using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditablenEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddDbContext<IOrderingDataContext, OrderingDataContext>((sp, opts) =>
            {
                opts.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opts.UseSqlServer(connectionString, contextBuilder => contextBuilder.EnableRetryOnFailure(5));
            });

            return services;
        }
    }
}
