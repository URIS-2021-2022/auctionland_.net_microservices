﻿// <auto-generated />
using System;
using Komisija_Agregat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Komisija_Agregat.Migrations
{
    [DbContext(typeof(KomisijaContext))]
    partial class KomisijaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Komisija_Agregat.Entities.ClanKomisije", b =>
                {
                    b.Property<Guid>("ClanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailClana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImeClana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrezimeClana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClanId");

                    b.ToTable("ClanoviKomisije");

                    b.HasData(
                        new
                        {
                            ClanId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            EmailClana = "jocko@mail.com",
                            ImeClana = "Nenad",
                            PrezimeClana = "Jeckovic"
                        });
                });

            modelBuilder.Entity("Komisija_Agregat.Entities.Komisija", b =>
                {
                    b.Property<Guid>("KomisijaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Clanovi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Predsednik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KomisijaId");

                    b.ToTable("Komisije");

                    b.HasData(
                        new
                        {
                            KomisijaId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            Clanovi = "sdd",
                            Predsednik = "sd"
                        });
                });

            modelBuilder.Entity("Komisija_Agregat.Entities.Predsednik", b =>
                {
                    b.Property<Guid>("PredsednikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailPredsednika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImePredsednika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrezimePredsednika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PredsednikId");

                    b.ToTable("Predsednici");

                    b.HasData(
                        new
                        {
                            PredsednikId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            EmailPredsednika = "markuza@mail.com",
                            ImePredsednika = "Petar",
                            PrezimePredsednika = "Markovic"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
