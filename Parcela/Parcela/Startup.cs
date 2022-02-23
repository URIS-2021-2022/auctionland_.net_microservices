using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela
{
    /// <summary>
    /// Startup klasa
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Konstruktor startup klase
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Deklaracija Configuration-a
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices metoda
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(setup =>
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState);

                    problemDetails.Detail = "Pogledajte polje errors za detalje.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    var actionExecutiongContext = context as ActionExecutingContext;

                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://google.com";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Doslo je do greske prilikom validacije.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };

                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Došlo je do greske prilikom parsiranja poslatog sadrzaja.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDeoParceleRepository, DeoParceleRepository>();
            services.AddScoped<IParcelaRepository, ParcelaRepository>();
            services.AddScoped<IOpstinaRepository, OpstinaRepository>();

            services.AddScoped<ILoggerService, LoggerService>();

            //services.AddDbContextPool<ParcelaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ParceleDB")));
            //services.AddDbContextPool<DeoParceleContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DeloviParceleDB")));
            //services.AddDbContextPool<OpstinaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OpstineDB")));

            //services.AddDbContext<ParcelaContext>(options => options.UseSqlServer(Configuration.GetConnectionString(nameof(ParcelaContext))));
            //services.AddDbContext<DeoParceleContext>(options => options.UseSqlServer(Configuration.GetConnectionString(nameof(DeoParceleContext))));
            //services.AddDbContext<OpstinaContext>(options => options.UseSqlServer(Configuration.GetConnectionString(nameof(OpstineContext))));

            services.AddDbContext<ParcelaContext>();

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ParcelaCreationValidator>());
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ParcelaUpdateValidator>());
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DeoParceleCreationValidator>());
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DeoParceleUpdateValidator>());
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OpstinaCreationValidator>());
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OpstinaUpdateValidator>());

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ParcelaOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Parcela service API",
                        Version = "1",
                        Description = "Pomocu ovog API-ja moze da se vrsi pregled, prisanje, dodavenje i modifikacija parceli i delova parcele.",
                    });
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

        }

        /// <summary>
        /// Configure metoda
        /// </summary>
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
                        await context.Response.WriteAsync("Doslo je do neocekivane greske. Pokusajte kasnije.");
                    });
                });
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ParcelaOpenApiSpecification/swagger.json", "Parcela API");
                //setupAction.RoutePrefix = "";
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
