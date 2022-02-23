﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parcela.Entities;

namespace Parcela.Migrations
{
    [DbContext(typeof(ParcelaContext))]
    partial class ParcelaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parcela.Entities.DeoParcele", b =>
                {
                    b.Property<Guid>("DeoParceleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParcelaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParcelaMParcelaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PovrsinaDelaParcele")
                        .HasColumnType("int");

                    b.Property<int>("RbrDelaParcele")
                        .HasColumnType("int");

                    b.HasKey("DeoParceleId");

                    b.HasIndex("ParcelaMParcelaId");

                    b.ToTable("DeloviParcele");

                    b.HasData(
                        new
                        {
                            DeoParceleId = new Guid("21ad52f8-0281-4241-98b0-481566d25e5f"),
                            ParcelaId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            PovrsinaDelaParcele = 1000,
                            RbrDelaParcele = 1
                        },
                        new
                        {
                            DeoParceleId = new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"),
                            ParcelaId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            PovrsinaDelaParcele = 1000,
                            RbrDelaParcele = 1
                        });
                });

            modelBuilder.Entity("Parcela.Entities.Opstina", b =>
                {
                    b.Property<Guid>("OpstinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivOpstine")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OpstinaId");

                    b.ToTable("Opstine");

                    b.HasData(
                        new
                        {
                            OpstinaId = new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                            NazivOpstine = "Bajmok"
                        },
                        new
                        {
                            OpstinaId = new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"),
                            NazivOpstine = "Bikovo"
                        },
                        new
                        {
                            OpstinaId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                            NazivOpstine = "Novi Grad"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.ParcelaM", b =>
                {
                    b.Property<Guid>("ParcelaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojListaNepokretnosti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojParcele")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KatastarskaOpstina")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Klasa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KlasaStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("KorisnikParcele")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Kultura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KulturaStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OblikSvojine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Obradivost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObradivostStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Odvodnjavanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OdvodnjavanjeStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OpstinaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Povrsina")
                        .HasColumnType("int");

                    b.Property<string>("ZasticenaZona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZasticenaZonaStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParcelaId");

                    b.HasIndex("OpstinaId");

                    b.ToTable("Parcele");

                    b.HasData(
                        new
                        {
                            ParcelaId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            BrojListaNepokretnosti = "1234",
                            BrojParcele = "12a",
                            KatastarskaOpstina = new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"),
                            Klasa = "I",
                            KlasaStvarnoStanje = "II",
                            Kultura = "Vrtovi",
                            KulturaStvarnoStanje = "Vrtovi",
                            OblikSvojine = "Privatna svojina",
                            Obradivost = "Ostalo",
                            ObradivostStvarnoStanje = "Ostalo",
                            Odvodnjavanje = "ostalo",
                            OdvodnjavanjeStvarnoStanje = "ostalo",
                            Povrsina = 10000,
                            ZasticenaZona = "3",
                            ZasticenaZonaStvarnoStanje = "4"
                        },
                        new
                        {
                            ParcelaId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            BrojListaNepokretnosti = "12345435",
                            BrojParcele = "15a",
                            KatastarskaOpstina = new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"),
                            Klasa = "II",
                            KlasaStvarnoStanje = "II",
                            Kultura = "Njive",
                            KulturaStvarnoStanje = "Njive",
                            OblikSvojine = "Privatna svojina",
                            Obradivost = "Obradivo",
                            ObradivostStvarnoStanje = "Ostalo",
                            Odvodnjavanje = "ostalo",
                            OdvodnjavanjeStvarnoStanje = "ostalo",
                            Povrsina = 3000,
                            ZasticenaZona = "3",
                            ZasticenaZonaStvarnoStanje = "4"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.DeoParcele", b =>
                {
                    b.HasOne("Parcela.Entities.ParcelaM", null)
                        .WithMany("DeloviParcele")
                        .HasForeignKey("ParcelaMParcelaId");
                });

            modelBuilder.Entity("Parcela.Entities.ParcelaM", b =>
                {
                    b.HasOne("Parcela.Entities.Opstina", null)
                        .WithMany("Parcele")
                        .HasForeignKey("OpstinaId");
                });

            modelBuilder.Entity("Parcela.Entities.Opstina", b =>
                {
                    b.Navigation("Parcele");
                });

            modelBuilder.Entity("Parcela.Entities.ParcelaM", b =>
                {
                    b.Navigation("DeloviParcele");
                });
#pragma warning restore 612, 618
        }
    }
}
