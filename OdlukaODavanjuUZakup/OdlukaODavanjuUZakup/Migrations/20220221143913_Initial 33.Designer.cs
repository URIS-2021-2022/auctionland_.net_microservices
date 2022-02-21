﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OdlukaODavanjuUZakup.Entities;

namespace OdlukaODavanjuUZakup.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220221143913_Initial 33")]
    partial class Initial33
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OdlukaODavanjuUZakup.Entities.GarantPlacanja", b =>
                {
                    b.Property<Guid>("GarantPlacanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Opis_garanta1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis_garanta2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GarantPlacanjaID");

                    b.ToTable("GarantPlacanja");

                    b.HasData(
                        new
                        {
                            GarantPlacanjaID = new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                            Opis_garanta1 = "Jemstvo",
                            Opis_garanta2 = "Jemstvo"
                        });
                });

            modelBuilder.Entity("OdlukaODavanjuUZakup.Entities.OdlukaoDavanjuuZakup", b =>
                {
                    b.Property<Guid>("OdlukaoDavanjuuZakupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("datum_donosenja_odluke")
                        .HasColumnType("datetime2");

                    b.Property<bool>("validnost")
                        .HasColumnType("bit");

                    b.HasKey("OdlukaoDavanjuuZakupID");

                    b.ToTable("OdlukaoDavanjuuZakup");

                    b.HasData(
                        new
                        {
                            OdlukaoDavanjuuZakupID = new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                            datum_donosenja_odluke = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            validnost = true
                        });
                });

            modelBuilder.Entity("OdlukaODavanjuUZakup.Entities.UgovoroZakupu", b =>
                {
                    b.Property<Guid>("UgovoroZakupuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Javno_Nadmetanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("datum_potpisa")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("datum_zavodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("lice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mesto_potpisivanja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("odluka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("rok_za_vracanje_zemljista")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("rokovi_dospeca")
                        .HasColumnType("datetime2");

                    b.Property<string>("tip_garancije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zavodni_Broj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UgovoroZakupuID");

                    b.ToTable("UgovoroZakupu");

                    b.HasData(
                        new
                        {
                            UgovoroZakupuID = new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                            datum_potpisa = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            datum_zavodjenja = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            lice = "test",
                            mesto_potpisivanja = "VrbaS",
                            odluka = "test",
                            rok_za_vracanje_zemljista = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            rokovi_dospeca = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            tip_garancije = "Jemstvo",
                            zavodni_Broj = "test"
                        });
                });

            modelBuilder.Entity("OdlukaODavanjuUZakup.Entities.UplataZakupnine", b =>
                {
                    b.Property<Guid>("UplataZakupnineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("broj_racuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("datum")
                        .HasColumnType("datetime2");

                    b.Property<double>("iznos")
                        .HasColumnType("float");

                    b.Property<string>("javno_nadmetanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("poziv_na_broj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("svrha_uplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("uplatilac")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UplataZakupnineID");

                    b.ToTable("UplataZakupnine");

                    b.HasData(
                        new
                        {
                            UplataZakupnineID = new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                            broj_racuna = "11222333444",
                            datum = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            iznos = 1000.0,
                            javno_nadmetanje = "javno",
                            poziv_na_broj = "15000",
                            svrha_uplate = "svrha",
                            uplatilac = "uplatilac"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
