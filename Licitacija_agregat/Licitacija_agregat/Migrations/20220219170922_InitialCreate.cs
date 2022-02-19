using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licitacija_agregat.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etape",
                columns: table => new
                {
                    EtapaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojEtape = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etape", x => x.EtapaId);
                });

            migrationBuilder.CreateTable(
                name: "Licitacije",
                columns: table => new
                {
                    LicitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ogranicenje = table.Column<int>(type: "int", nullable: false),
                    Korak_cene = table.Column<int>(type: "int", nullable: false),
                    Rok_za_dostavljanje_prijave = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacije", x => x.LicitacijaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Etape");

            migrationBuilder.DropTable(
                name: "Licitacije");
        }
    }
}
