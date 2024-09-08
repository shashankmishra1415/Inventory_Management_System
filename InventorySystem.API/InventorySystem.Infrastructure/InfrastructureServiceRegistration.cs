using InventorySystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace InventorySystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext>();
            //SqlMapper.AddTypeHandler(typeof(List<UserTypeResponse>), new UserTypeHandler());
            //SqlMapper.AddTypeHandler(typeof(UserTypeAndCity), new UserTypeAndCityHandler());

            return services;
        }
    }
}