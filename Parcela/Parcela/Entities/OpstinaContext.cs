using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Kontekst za opstine
    /// </summary>
    public class OpstinaContext : DbContext
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Konstruktor konteksta za opstine
        /// </summary>
        public OpstinaContext(DbContextOptions<OpstinaContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<Opstina> Opstine { get; set; }

        /// <summary>
        /// Metoda OnConfiguring
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OpstineDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Opstina>()
                .HasData(new
                {
                    OpstinaId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivOpstine = "Bajmok"
                });

            builder.Entity<Opstina>()
                .HasData(new
                {
                    OpstinaId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    NazivOpstine = "Bikovo"
                });

            builder.Entity<Opstina>()
                .HasData(new
                {
                    OpstinaId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    NazivOpstine = "Novi Grad"
                });
        }
    }
}
