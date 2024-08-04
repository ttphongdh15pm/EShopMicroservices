using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            return services;
        }
    }
}
