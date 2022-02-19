using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oglas_Agregat.Data;
using System.Reflection;
using System.IO;

namespace Oglas_Agregat
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
            }
            ).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //pogledaj ceo domen i trazi konfiguracije za automaper. to su profili. mi za svako mapiranje definisemo profil tj iz tog objekta mi mapiraj u taj objekat na taj nacin

            services.AddSingleton<IOglasRepository, OglasRepository>(); //kada sretnes da se trazi prvo prosledi drugo tj napravi instancu drugog i koristi je, singleton je zivotni ciklus drugog
            services.AddSingleton<ISluzbeniListRepository, SluzbeniListRepository>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("OglasOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Oglas API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja se vrši kreiranje, modifikacija i pregled kreiranih oglasa i službenih listova.",
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                setupAction.SwaggerEndpoint("/swagger/OglasOpenApiSpecification/swagger.json", "Oglas API");
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
