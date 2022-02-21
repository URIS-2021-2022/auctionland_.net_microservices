using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    /// <summary>
    /// Kontekst za tip zalbe
    /// </summary>
    public class TipZalbeContext : DbContext
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Konstruktor konteksta za tip zalbe
        /// </summary>
        public TipZalbeContext(DbContextOptions<TipZalbeContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<TipZalbe> TipoviZalbi { get; set; }

        /// <summary>
        /// Metoda OnConfiguring
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TipoviZalbiDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivTipa = "Žalba na tok javnog nadmetanaja"
                });

            builder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    NazivTipa = "Žalba na Odluku o davanju u zakup"
                });

            builder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    NazivTipa = "Žalba na Odluku o davanju na korišćenje"
                });
        }
    }
}
