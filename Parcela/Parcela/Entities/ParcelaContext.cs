using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Kontekst za parcele
    /// </summary>
    public class ParcelaContext : DbContext
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Konstruktor konteksta za parcele
        /// </summary>
        public ParcelaContext(DbContextOptions<ParcelaContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<ParcelaM> Parcele { get; set; }
        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<Opstina> Opstine { get; set; }
        /// <summary>
        /// Deklaracija eniteta za smestanje u bazu
        /// </summary>
        public DbSet<DeoParcele> DeloviParcele { get; set; }

        /// <summary>
        /// Metoda OnConfiguring
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ParceleDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Opstina>()
                .HasData(new
                {
                    OpstinaId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivOpstine = "Bajmok"
                });

            modelBuilder.Entity<Opstina>()
                .HasData(new
                {
                    OpstinaId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    NazivOpstine = "Bikovo"
                });

            modelBuilder.Entity<Opstina>()
                .HasData(new
                {
                    OpstinaId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    NazivOpstine = "Novi Grad"
                });


            modelBuilder.Entity<ParcelaM>()
                .HasData(new
                {
                    ParcelaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Povrsina = 10000,
                    KorisnikParcele = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a55"),
                    BrojParcele = "12a",
                    KatastarskaOpstina = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    BrojListaNepokretnosti = "1234",
                    Kultura = "Vrtovi",
                    Klasa = "I",
                    Obradivost = "Ostalo",
                    ZasticenaZona = "3",
                    OblikSvojine = "Privatna svojina",
                    Odvodnjavanje = "ostalo",
                    KulturaStvarnoStanje = "Vrtovi",
                    KlasaStvarnoStanje = "II",
                    ObradivostStvarnoStanje = "Ostalo",
                    ZasticenaZonaStvarnoStanje = "4",
                    OdvodnjavanjeStvarnoStanje = "ostalo"
                });

            modelBuilder.Entity<ParcelaM>()
                .HasData(new
                {
                    ParcelaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Povrsina = 3000,
                    KorisnikParcele = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a55"),
                    BrojParcele = "15a",
                    KatastarskaOpstina = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    BrojListaNepokretnosti = "12345435",
                    Kultura = "Njive",
                    Klasa = "II",
                    Obradivost = "Obradivo",
                    ZasticenaZona = "3",
                    OblikSvojine = "Privatna svojina",
                    Odvodnjavanje = "ostalo",
                    KulturaStvarnoStanje = "Njive",
                    KlasaStvarnoStanje = "II",
                    ObradivostStvarnoStanje = "Ostalo",
                    ZasticenaZonaStvarnoStanje = "4",
                    OdvodnjavanjeStvarnoStanje = "ostalo"

                });



            modelBuilder.Entity<DeoParcele>()
                .HasData(new
                {
                    DeoParceleId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e5f"),
                    ParcelaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PovrsinaDelaParcele = 1000,
                    RbrDelaParcele = 1
                });

            modelBuilder.Entity<DeoParcele>()
                .HasData(new
                {
                    DeoParceleId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    ParcelaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    PovrsinaDelaParcele = 1000,
                    RbrDelaParcele = 1
                });
        }

    }
}
