using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Entities
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<GarantPlacanja> GarantPlacanja { get; set; }
        public DbSet<UgovoroZakupu> UgovoroZakupu { get; set; }
        public DbSet<OdlukaoDavanjuuZakup> OdlukaoDavanjuuZakup { get; set; }
        public DbSet<UplataZakupnine> UplataZakupnine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OdlukaDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GarantPlacanja>()
                .HasData(new
                {
                    GarantPlacanjaID = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    Opis_garanta1 = "Jemstvo",
                    Opis_garanta2 = "Jemstvo"
                });
            modelBuilder.Entity<UplataZakupnine>()
                .HasData(new
                {
                    UplataZakupnineID = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    broj_racuna = "11222333444",
                    datum = DateTime.Parse("02-02-2000"),
                    iznos = 1000.00,
                    poziv_na_broj = "15000",
                    svrha_uplate = "svrha",
                    javno_nadmetanje = "javno",
                    uplatilac = "uplatilac"
                });
            modelBuilder.Entity<OdlukaoDavanjuuZakup>()
                .HasData(new
                {
                    OdlukaoDavanjuuZakupID = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    datum_donosenja_odluke = DateTime.Parse("02-02-2000"),
                    validnost = true
                });
            modelBuilder.Entity<UgovoroZakupu>()
                .HasData(new
                {
                    UgovoroZakupuID = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    javno_nadmetanje = "javno",
                    odluka = "test",
                    tip_garancije = "Jemstvo",
                    lice = "test",
                    zavodni_Broj = "test",
                    mesto_potpisivanja = "VrbaS",
                    datum_potpisa = DateTime.Parse("02-02-2000"),
                    rok_za_vracanje_zemljista = DateTime.Parse("02-02-2000"),
                    rokovi_dospeca = DateTime.Parse("02-02-2000"),
                    datum_zavodjenja = DateTime.Parse("02-02-2000")
                });
        }
                

        }
    }
