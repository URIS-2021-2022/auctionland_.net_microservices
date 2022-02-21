using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Kontekst za deo parcele
    /// </summary>
    public class DeoParceleContext : DbContext
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Konstruktor konteksta za deo parcele
        /// </summary>
        public DeoParceleContext(DbContextOptions<DeoParceleContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<DeoParcele> DeloviParcele { get; set; }

        /// <summary>
        /// Metoda OnConfiguring
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ParcelaDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DeoParcele>()
                .HasData(new
                {
                    DeoParceleId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e5f"),
                    ParcelaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PovrsinaDelaParcele = "1000ha",
                    RbrDelaParcele = 1
                });

            builder.Entity<DeoParcele>()
                .HasData(new
                {
                    DeoParceleId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    ParcelaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    PovrsinaDelaParcele = "1000ha",
                    RbrDelaParcele = 1
                });

        }
    }
}
