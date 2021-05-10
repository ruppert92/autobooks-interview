using GroceryStore.BLL.Interfaces;
using GroceryStore.BLL.Services;
using GroceryStore.DAL.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.BLL.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureGroceryStoreBLL(this IServiceCollection services)
        {
            services.ConfigureGroceryStoreDAL();
            services.AddScoped<ICustomersService, CustomersService>();

            return services;
        }
    }
}
