using Trading_API.Infrastructure;
using Trading_API.Interfaces;
using Trading_API.Repository;

namespace Trading_API
{
    public static class MyConfigServiceCollection
    {
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            //Database Connection
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            //----------------------------------------------
            //User Repository
            services.AddScoped<IUserRepository, UserRepository>();
            //----------------------------------------------
            //Currency Repository
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            //----------------------------------------------
            services.AddScoped<IIpozitRepository, IpozitRepository>();
            //----------------------------------------------
            services.AddScoped<ISoldRepository, SoldRepository>();
            //----------------------------------------------
            
            return services;
        }
    }
}
