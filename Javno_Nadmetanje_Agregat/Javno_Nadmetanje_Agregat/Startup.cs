using Javno_Nadmetanje_Agregat.Data;
using Javno_Nadmetanje_Agregat.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Javno_Nadmetanje_Agregat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<IJavnoNadmetanjeRepository, JavnoNadmetanjeRepository>();
            services.AddScoped<ITipJavnogNadmetanjaRepository, TipJavnogNadmetanjaRepository>();
            services.AddScoped<IStatusJavnogNadmetanjaRepository, StatusJavnogNadmetanjaRepository>();

            services.AddControllers();

            services.AddDbContext<JavnoNadmetanjeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("JavnoNadmetanjeDB")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("JavnoNadmetanjeOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Javno Nadmetanje API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja se vrši kreiranje, modifikacija i pregled kreiranih javnih nadmetanja.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Nikolina Vuković",
                            Email = "nikolina.vukovic@uns.ac.rs"
                        }
                    });

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml"; //kupimo gde se nalazi nas fajl, mozemo zbog onoga sto smo napisali u proj properties
                            var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments); //nalepimo na putanju

                            setupAction.IncludeXmlComments(xmlCommentsPath);
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/JavnoNadmetanjeOpenApiSpecification/swagger.json", "Javno Nadmetanje API");
                setupAction.RoutePrefix = ""; //da se na rootu pokrece dokumentacija
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
