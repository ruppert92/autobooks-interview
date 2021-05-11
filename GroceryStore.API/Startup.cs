using AutoMapper;
using GroceryStore.API.Middleware;
using GroceryStore.BLL.IoC;
using GroceryStore.Common.Configuration;
using GroceryStore.Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GroceryStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public GroceryStoreConfiguration GroceryStoreConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var groceryStoreConfiguration = new GroceryStoreConfiguration();
            Configuration.GetSection("GroceryStore").Bind(groceryStoreConfiguration);
            GroceryStoreConfiguration = groceryStoreConfiguration;

            services.AddSingleton<GroceryStoreConfiguration>(x => groceryStoreConfiguration);

            // automapper
            services.AddScoped<IMapper>(x => AutoMapperHelper.ConfigureAutomapper().CreateMapper());
            services.ConfigureGroceryStoreBLL();

            services.AddControllers(configure => configure.Filters.Add(typeof(ExceptionInterceptor)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GroceryStore.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroceryStore.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
