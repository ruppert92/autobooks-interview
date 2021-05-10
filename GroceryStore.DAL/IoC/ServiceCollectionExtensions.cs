using GroceryStore.Common.Configuration;
using GroceryStore.DAL.Contexts;
using GroceryStore.DAL.Interfaces;
using GroceryStore.DAL.Models;
using GroceryStore.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.DAL.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureGroceryStoreDAL(this IServiceCollection services)
        {
            services.AddScoped<IGroceryStoreDbContext>((provider) => new GroceryStoreFileDbContext(provider.GetRequiredService<GroceryStoreConfiguration>().GroceryStoreDbFilePath));
            services.AddScoped<IGroceryStoreRepository<Customer>, GroceryStoryRepository<Customer>>();

            return services;
        }
    }
}
