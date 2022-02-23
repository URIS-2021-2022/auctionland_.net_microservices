using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    /// <summary>
    /// Kontekst za zalbe
    /// </summary>
    public class ZalbaContext : DbContext
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Konstruktor konteksta za zalbe
        /// </summary>
        public ZalbaContext(DbContextOptions<ZalbaContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<ZalbaM> Zalbe { get; set; }
        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<TipZalbe> TipoviZalbi { get; set; }

        /// <summary>
        /// Metoda OnConfiguring
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ZalbeDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivTipa = "Žalba na tok javnog nadmetanaja"
                });

            modelBuilder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    NazivTipa = "Žalba na Odluku o davanju u zakup"
                });

            modelBuilder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    NazivTipa = "Žalba na Odluku o davanju na korišćenje"
                });




            modelBuilder.Entity<ZalbaM>()
                .HasData(new
                {
                    ZalbaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    TipId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    DatumPodnosenjaZalbe = DateTime.Parse("2020-11-15T09:00:00"),
                    //PodnosilacZalbe = Guid.Parse(""),
                    RazlogZalbe = "razlog",
                    Obrazlozenje = "obrazlozenje",
                    DatumResenja = DateTime.Parse("2022-02-20T09:00:00"),
                    BrojResenja = "S123",
                    StatusZalbe = "Otvorena",
                    BrojOdluke = "S001",
                    RadnjaNaOsnovuZalbe = "JN ne ide u drugi krug"
                });

            modelBuilder.Entity<ZalbaM>()
                .HasData(new
                {
                    ZalbaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    TipId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    DatumPodnosenjaZalbe = DateTime.Parse("2020-11-15T09:00:00"),
                    //PodnosilacZalbe = Guid.Parse(""),
                    RazlogZalbe = "razlog",
                    Obrazlozenje = "obrazlozenje",
                    DatumResenja = DateTime.Parse("2022-02-20T09:00:00"),
                    BrojResenja = "S124",
                    StatusZalbe = "Odbijena",
                    BrojOdluke = "S002",
                    RadnjaNaOsnovuZalbe = "JN ide u drugi krug sa novim uslovima"
                });
        }
    }
}
