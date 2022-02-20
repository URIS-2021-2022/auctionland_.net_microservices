using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Entities
{
    public class OglasContext : DbContext
    {
        private readonly IConfiguration configuration;

        public OglasContext()
        {

        }

        public OglasContext(DbContextOptions<OglasContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Oglas> Oglasi { get; set; } //direktno se namapira na objekte u bazi
        public DbSet<SluzbeniList> SluzbeniListovi { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) //nakon sto mi pokrenemo izvrsavanje programa
        {
            builder.Entity<Oglas>()
                .HasData(new
                {
                    OglasId = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    OpisOglasa = "fdafdafa",
                    RokZaZalbu = DateTime.Parse("02-02-2000"),
                    DatumObjave = DateTime.Parse("01-01-2000")
                });

            builder.Entity<SluzbeniList>()
                .HasData(new
                {
                    SluzbeniListId = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    DatumIzdanja = DateTime.Parse("01-01-2001"),
                    BrojLista = 33
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OglasDB"));
        }
    }
}
