﻿// <auto-generated />
using System;
using Liciter___Agregat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Liciter___Agregat.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Liciter___Agregat.Models.FizickoLiceModel", b =>
                {
                    b.Property<Guid>("FizickoLiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FizickoLiceId");

                    b.ToTable("FizickaLica");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.KupacModel", b =>
                {
                    b.Property<Guid>("KupacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumPocetkaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumPrestankaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<int>("DuzinaTrajanjaZabraneGod")
                        .HasColumnType("int");

                    b.Property<Guid?>("FizickoLiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ImaZabranu")
                        .HasColumnType("bit");

                    b.Property<Guid?>("JavnoNadmetanjeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OstvarenaPovrsina")
                        .HasColumnType("int");

                    b.Property<Guid?>("PravnoliceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Prioritet")
                        .HasColumnType("int");

                    b.HasKey("KupacId");

                    b.HasIndex("FizickoLiceId");

                    b.HasIndex("PravnoliceId");

                    b.ToTable("Kupci");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.LiciterModel", b =>
                {
                    b.Property<Guid>("LiciterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("KupacId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OvlascenoLiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LiciterId");

                    b.HasIndex("KupacId");

                    b.HasIndex("OvlascenoLiceId");

                    b.ToTable("Liciteri");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.OvlascenoLiceModel", b =>
                {
                    b.Property<Guid>("OvlascenoLiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrojTable")
                        .HasColumnType("int");

                    b.Property<string>("Drzava")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG_Br_Pasosa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KupacId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OvlascenoLiceId");

                    b.HasIndex("KupacId");

                    b.ToTable("OvlascenaLica");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.PravnoLiceModel", b =>
                {
                    b.Property<Guid>("PravnoLiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KontaktOsoba")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaticniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PravnoLiceId");

                    b.ToTable("PravnaLica");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.KupacModel", b =>
                {
                    b.HasOne("Liciter___Agregat.Models.FizickoLiceModel", "FizickoLice")
                        .WithMany()
                        .HasForeignKey("FizickoLiceId");

                    b.HasOne("Liciter___Agregat.Models.PravnoLiceModel", "PravnoLice")
                        .WithMany()
                        .HasForeignKey("PravnoliceId");

                    b.Navigation("FizickoLice");

                    b.Navigation("PravnoLice");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.LiciterModel", b =>
                {
                    b.HasOne("Liciter___Agregat.Models.KupacModel", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacId");

                    b.HasOne("Liciter___Agregat.Models.OvlascenoLiceModel", "OvlascenoLice")
                        .WithMany()
                        .HasForeignKey("OvlascenoLiceId");

                    b.Navigation("Kupac");

                    b.Navigation("OvlascenoLice");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.OvlascenoLiceModel", b =>
                {
                    b.HasOne("Liciter___Agregat.Models.KupacModel", "Kupac")
                        .WithMany("OvlascenaLica")
                        .HasForeignKey("KupacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kupac");
                });

            modelBuilder.Entity("Liciter___Agregat.Models.KupacModel", b =>
                {
                    b.Navigation("OvlascenaLica");
                });
#pragma warning restore 612, 618
        }
    }
}
