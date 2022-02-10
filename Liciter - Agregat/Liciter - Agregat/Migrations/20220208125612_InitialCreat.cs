using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liciter___Agregat.Migrations
{
    public partial class InitialCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FizickoLiceModels",
                columns: table => new
                {
                    FizickoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FizickoLiceModels", x => x.FizickoLiceId);
                });

            migrationBuilder.CreateTable(
                name: "PravnoLiceModels",
                columns: table => new
                {
                    PravnoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaticniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktOsoba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravnoLiceModels", x => x.PravnoLiceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FizickoLiceModels");

            migrationBuilder.DropTable(
                name: "PravnoLiceModels");
        }
    }
}
