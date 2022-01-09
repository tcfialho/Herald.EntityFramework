using Herald.EntityFramework.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Herald.EntityFramework
{
    public static class Configurations
    {
        public static IServiceCollection AddHeraldEntityFramework<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddTransient<DbContext>(x => x.GetRequiredService<TContext>());
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
