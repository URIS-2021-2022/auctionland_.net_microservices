using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liciter___Agregat.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PravnoLiceModels",
                table: "PravnoLiceModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FizickoLiceModels",
                table: "FizickoLiceModels");

            migrationBuilder.RenameTable(
                name: "PravnoLiceModels",
                newName: "PravnaLica");

            migrationBuilder.RenameTable(
                name: "FizickoLiceModels",
                newName: "FizickaLica");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PravnaLica",
                table: "PravnaLica",
                column: "PravnoLiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FizickaLica",
                table: "FizickaLica",
                column: "FizickoLiceId");

            migrationBuilder.CreateTable(
                name: "Kupci",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prioritet = table.Column<int>(type: "int", nullable: false),
                    FizickoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PravnoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OstvarenaPovrsina = table.Column<int>(type: "int", nullable: false),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzinaTrajanjaZabraneGod = table.Column<int>(type: "int", nullable: false),
                    DatumPrestankaZabrane = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        name: "FK_Kupci_PravnaLica_PravnoLiceId",
                        column: x => x.PravnoLiceId,
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
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvlascenaLica", x => x.OvlascenoLiceId);
                });

            migrationBuilder.CreateTable(
                name: "JavnaNadmetanja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    javnoNadmetanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KupacModelKupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavnaNadmetanja", x => x.id);
                    table.ForeignKey(
                        name: "FK_JavnaNadmetanja_Kupci_KupacModelKupacId",
                        column: x => x.KupacModelKupacId,
                        principalTable: "Kupci",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uplata",
                columns: table => new
                {
                    UplataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    uplataString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KupacModelKupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplata", x => x.UplataId);
                    table.ForeignKey(
                        name: "FK_Uplata_Kupci_KupacModelKupacId",
                        column: x => x.KupacModelKupacId,
                        principalTable: "Kupci",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KupacModelOvlascenoLiceModel",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OvlascenaLicaOvlascenoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupacModelOvlascenoLiceModel", x => new { x.KupacId, x.OvlascenaLicaOvlascenoLiceId });
                    table.ForeignKey(
                        name: "FK_KupacModelOvlascenoLiceModel_Kupci_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupci",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupacModelOvlascenoLiceModel_OvlascenaLica_OvlascenaLicaOvlascenoLiceId",
                        column: x => x.OvlascenaLicaOvlascenoLiceId,
                        principalTable: "OvlascenaLica",
                        principalColumn: "OvlascenoLiceId",
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
                name: "IX_JavnaNadmetanja_KupacModelKupacId",
                table: "JavnaNadmetanja",
                column: "KupacModelKupacId");

            migrationBuilder.CreateIndex(
                name: "IX_KupacModelOvlascenoLiceModel_OvlascenaLicaOvlascenoLiceId",
                table: "KupacModelOvlascenoLiceModel",
                column: "OvlascenaLicaOvlascenoLiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupci_FizickoLiceId",
                table: "Kupci",
                column: "FizickoLiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupci_PravnoLiceId",
                table: "Kupci",
                column: "PravnoLiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Liciteri_KupacId",
                table: "Liciteri",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Liciteri_OvlascenoLiceId",
                table: "Liciteri",
                column: "OvlascenoLiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_KupacModelKupacId",
                table: "Uplata",
                column: "KupacModelKupacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JavnaNadmetanja");

            migrationBuilder.DropTable(
                name: "KupacModelOvlascenoLiceModel");

            migrationBuilder.DropTable(
                name: "Liciteri");

            migrationBuilder.DropTable(
                name: "Uplata");

            migrationBuilder.DropTable(
                name: "OvlascenaLica");

            migrationBuilder.DropTable(
                name: "Kupci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PravnaLica",
                table: "PravnaLica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FizickaLica",
                table: "FizickaLica");

            migrationBuilder.RenameTable(
                name: "PravnaLica",
                newName: "PravnoLiceModels");

            migrationBuilder.RenameTable(
                name: "FizickaLica",
                newName: "FizickoLiceModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PravnoLiceModels",
                table: "PravnoLiceModels",
                column: "PravnoLiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FizickoLiceModels",
                table: "FizickoLiceModels",
                column: "FizickoLiceId");
        }
    }
}
