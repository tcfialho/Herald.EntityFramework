using Herald.EntityFramework.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Herald.EntityFramework
{
    public static class Configurations
    {
        public static IServiceCollection AddHeraldEntityFramework<TContext>(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Scoped) where TContext : DbContext
        {
            services.Add(new ServiceDescriptor(typeof(DbContext), x => x.GetRequiredService<TContext>(), contextLifetime));
            services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), contextLifetime));
            return services;
        }
    }
}
