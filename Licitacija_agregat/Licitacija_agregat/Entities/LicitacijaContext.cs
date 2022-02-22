using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Entities
{
    public class LicitacijaContext : DbContext
    {
        private readonly IConfiguration configuration;

        public LicitacijaContext(DbContextOptions<LicitacijaContext> options, IConfiguration configuration) : base(options) 
        {
            this.configuration = configuration;
        }

        public LicitacijaContext()
        {

        }

        public DbSet<Licitacija> Licitacije { get; set; }
        public DbSet<Etapa> Etape{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LicitacijaDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Licitacija>()
                .HasData(new
                {
                    LicitacijaId = Guid.Parse("e1f1f516-a9c4-4209-baa7-02e1583484ce"),
                    Datum = DateTime.Parse("2023-11-15"),
                    Rok_za_dostavljanje_prijave = DateTime.Parse("2023-11-15"),
                    Broj = 5,
                    Godina = 2005,
                    Ogranicenje = 5,
                    Korak_cene = 5
                });

        }

    }
}
