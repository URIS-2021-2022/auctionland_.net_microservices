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
        /// Metoda OnConfiguring
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ZalbaDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ZalbaM>()
                .HasData(new
                {
                    ZalbaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Tip = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
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

            builder.Entity<ZalbaM>()
                .HasData(new
                {
                    ZalbaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Tip = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
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
