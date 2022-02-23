using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Korisnik_agregat.Entities
{
    public class KorisnikContext : DbContext
    {
        private readonly IConfiguration configuration;

        public KorisnikContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<TipKorisnika> TipoviKorisnika { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("KorisnikDB"));
        }
        private Tuple<string, string> HashPassword(string lozinka)
        {
            var sBytes = new byte[lozinka.Length];

            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);

            var salt = Convert.ToBase64String(sBytes);
            var derivedBytes = new Rfc2898DeriveBytes(lozinka, sBytes, 100);

            return new Tuple<string, string>(Convert.ToBase64String(derivedBytes.GetBytes(256)), salt);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipKorisnika>()
                .HasData(
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("411badd1-bee5-4bf4-9eab-138806fa8914"),
                        NazivTipa = "Operater"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("62d026f1-caf7-4eae-bf1a-261a95f7f0bb"),
                        NazivTipa = "Tehnički sekretar"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("6216bfa0-66d0-48e8-9014-ad1030f10140"),
                        NazivTipa = "Prva komisija"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("b02616e3-e14d-4548-ba2b-c064b6f21a98"),
                        NazivTipa = "Superuser"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("d7a63d05-c1f3-4936-9b70-f0b2b496d297"),
                        NazivTipa = "Operater nadmetanja"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("bcafc2c9-378b-42a5-8fb7-10a0c4726a22"),
                        NazivTipa = "Licitant"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("3634f986-0503-4369-a7ea-891517db5084"),
                        NazivTipa = "Menadžer"
                    },
                    new TipKorisnika
                    {
                        TipKorisnikaId = Guid.Parse("577a8f2b-1a55-4e91-a3ea-3d5cf16814a6"),
                        NazivTipa = "Administrator"
                    }
                );

            var korisnik = HashPassword("urisftnftn");

            modelBuilder.Entity<Korisnik>()
                .HasData(
                    new Korisnik
                    {
                        KorisnikId = Guid.Parse("34f11383-cb12-481d-9ff7-2fd458dc7e2b"),
                        Ime = "Vlado",
                        Prezime = "Cetkovic",
                        KorisnickoIme = "Vlado",
                        Lozinka = korisnik.Item1,
                        TipKorisnikaId = Guid.Parse("577a8f2b-1a55-4e91-a3ea-3d5cf16814a6"),
                        Salt = korisnik.Item2
                    }
                );
        }
    }
}
