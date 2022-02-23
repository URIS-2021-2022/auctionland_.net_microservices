using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licitacija_agregat.Migrations
{
    public partial class ic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Etape",
                columns: table => new
                {
                    EtapaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojEtape = table.Column<int>(type: "int", nullable: false),
                    LicitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etape", x => x.EtapaId);
                    table.ForeignKey(
                        name: "FK_Etape_Licitacije_LicitacijaId",
                        column: x => x.LicitacijaId,
                        principalTable: "Licitacije",
                        principalColumn: "LicitacijaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Licitacije",
                columns: new[] { "LicitacijaId", "Broj", "Datum", "Godina", "Korak_cene", "Ogranicenje", "Rok_za_dostavljanje_prijave" },
                values: new object[] { new Guid("e1f1f516-a9c4-4209-baa7-02e1583484ce"), 5, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2005, 5, 5, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Etape_LicitacijaId",
                table: "Etape",
                column: "LicitacijaId");
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
