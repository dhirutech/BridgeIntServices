using BridgeIntServices.Core.Interfaces;
using BridgeIntServices.Core.Logics;
using BridgeIntServices.Interfaces;
using BridgeIntServices.Logics;
using BridgeIntServices.Repositories.Interfaces;
using BridgeIntServices.Repositories.Logics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BridgeIntServices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IBatteryUsageLogics, BatteryUsageLogics>();
            services.AddScoped<IResponseHelper, ResponseHelper>();
            services.AddScoped<IBridgeLogger, BridgeLogger>();
            services.AddScoped<IBatteryUsageRepository, BatteryUsageRepository>();

            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BridgeIntServices",
                    Description = "Bridge International Academies - Fullstack Developer Technical Test - Teacher Tablet Battery Usage",
                    Contact = new OpenApiContact
                    {
                        Name = "Dhiraj Bairagi",
                        Email = "dhiraj.bairagi@yahoo.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "BridgeIntServices"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "BridgeIntServices");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
