using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liciter___Agregat.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FizickaLica",
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
                    table.PrimaryKey("PK_FizickaLica", x => x.FizickoLiceId);
                });

            migrationBuilder.CreateTable(
                name: "PravnaLica",
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
                    table.PrimaryKey("PK_PravnaLica", x => x.PravnoLiceId);
                });

            migrationBuilder.CreateTable(
                name: "Kupci",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prioritet = table.Column<int>(type: "int", nullable: false),
                    OstvarenaPovrsina = table.Column<int>(type: "int", nullable: false),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzinaTrajanjaZabraneGod = table.Column<int>(type: "int", nullable: false),
                    DatumPrestankaZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FizickoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PravnoliceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JavnoNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupci", x => x.KupacId);
                    table.ForeignKey(
                        name: "FK_Kupci_FizickaLica_FizickoLiceId",
                        column: x => x.FizickoLiceId,
                        principalTable: "FizickaLica",
                        principalColumn: "FizickoLiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kupci_PravnaLica_PravnoliceId",
                        column: x => x.PravnoliceId,
                        principalTable: "PravnaLica",
                        principalColumn: "PravnoLiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OvlascenaLica",
                columns: table => new
                {
                    OvlascenoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JMBG_Br_Pasosa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvlascenaLica", x => x.OvlascenoLiceId);
                    table.ForeignKey(
                        name: "FK_OvlascenaLica_Kupci_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupci",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Liciteri",
                columns: table => new
                {
                    LiciterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OvlascenoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liciteri", x => x.LiciterId);
                    table.ForeignKey(
                        name: "FK_Liciteri_Kupci_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupci",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Liciteri_OvlascenaLica_OvlascenoLiceId",
                        column: x => x.OvlascenoLiceId,
                        principalTable: "OvlascenaLica",
                        principalColumn: "OvlascenoLiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kupci_FizickoLiceId",
                table: "Kupci",
                column: "FizickoLiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupci_PravnoliceId",
                table: "Kupci",
                column: "PravnoliceId");

            migrationBuilder.CreateIndex(
                name: "IX_Liciteri_KupacId",
                table: "Liciteri",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Liciteri_OvlascenoLiceId",
                table: "Liciteri",
                column: "OvlascenoLiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OvlascenaLica_KupacId",
                table: "OvlascenaLica",
                column: "KupacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Liciteri");

            migrationBuilder.DropTable(
                name: "OvlascenaLica");

            migrationBuilder.DropTable(
                name: "Kupci");

            migrationBuilder.DropTable(
                name: "FizickaLica");

            migrationBuilder.DropTable(
                name: "PravnaLica");
        }
    }
}
