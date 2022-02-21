using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OdlukaODavanjuUZakup.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IOdlukaoDavanjuuZakupRepository, OdlukaoDavanjuuZakupuRepository>();
            services.AddSingleton<IGarantPlacanjaRepository, GarantPlacanjaRepository>();
            services.AddSingleton<IUplataZakupnineRepository, UplataZakupnineRepository>();
            services.AddSingleton<IUgovoroZakupuRepository, UgovoroZakupuRepository>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("OdlukaODavanjuUZakupOpenApiSpecification",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Odluka o davanju u zakup API",
                    Version = "1",
                    Description = "Mikroservis koji je deo AuctionLand projekta",

                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Радоња Милутиновић",
                        Email = "Radonja@uns.ac.rs",
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Ftn licenca"
                    }
                }); ;
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                  {
                      context.Response.StatusCode = 500;
                      await context.Response.WriteAsync("Doslo je do neocekivane greske. Molimo pokusajte kasnije");
                  });
                });
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/OdlukaODavanjuUZakupOpenApiSpecification/swagger.json", "Zakup API");
                setupAction.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
