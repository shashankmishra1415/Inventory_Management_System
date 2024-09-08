using FluentValidation;
using InventorySystem.Application.Validator;

namespace InventorySystem.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureValidators(this IServiceCollection services)
        {
            //With Scope
            services.AddValidatorsFromAssemblyContaining<WarehouseValidator>(ServiceLifetime.Transient);
            return services;
        }
    }
}
