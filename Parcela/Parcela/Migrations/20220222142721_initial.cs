using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parcela.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opstine",
                columns: table => new
                {
                    OpstinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivOpstine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opstine", x => x.OpstinaId);
                });

            migrationBuilder.CreateTable(
                name: "Parcele",
                columns: table => new
                {
                    ParcelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Povrsina = table.Column<int>(type: "int", nullable: false),
                    BrojParcele = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KatastarskaOpstina = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojListaNepokretnosti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kultura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Klasa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obradivost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZasticenaZona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OblikSvojine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odvodnjavanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KulturaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KlasaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObradivostStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZasticenaZonaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdvodnjavanjeStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpstinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcele", x => x.ParcelaId);
                    table.ForeignKey(
                        name: "FK_Parcele_Opstine_OpstinaId",
                        column: x => x.OpstinaId,
                        principalTable: "Opstine",
                        principalColumn: "OpstinaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeloviParcele",
                columns: table => new
                {
                    DeoParceleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PovrsinaDelaParcele = table.Column<int>(type: "int", nullable: false),
                    RbrDelaParcele = table.Column<int>(type: "int", nullable: false),
                    ParcelaMParcelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeloviParcele", x => x.DeoParceleId);
                    table.ForeignKey(
                        name: "FK_DeloviParcele_Parcele_ParcelaMParcelaId",
                        column: x => x.ParcelaMParcelaId,
                        principalTable: "Parcele",
                        principalColumn: "ParcelaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DeloviParcele",
                columns: new[] { "DeoParceleId", "ParcelaId", "ParcelaMParcelaId", "PovrsinaDelaParcele", "RbrDelaParcele" },
                values: new object[,]
                {
                    { new Guid("21ad52f8-0281-4241-98b0-481566d25e5f"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), null, 1000, 1 },
                    { new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), null, 1000, 1 }
                });

            migrationBuilder.InsertData(
                table: "Opstine",
                columns: new[] { "OpstinaId", "NazivOpstine" },
                values: new object[,]
                {
                    { new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "Bajmok" },
                    { new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "Bikovo" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "Novi Grad" }
                });

            migrationBuilder.InsertData(
                table: "Parcele",
                columns: new[] { "ParcelaId", "BrojListaNepokretnosti", "BrojParcele", "KatastarskaOpstina", "Klasa", "KlasaStvarnoStanje", "Kultura", "KulturaStvarnoStanje", "OblikSvojine", "Obradivost", "ObradivostStvarnoStanje", "Odvodnjavanje", "OdvodnjavanjeStvarnoStanje", "OpstinaId", "Povrsina", "ZasticenaZona", "ZasticenaZonaStvarnoStanje" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "1234", "12a", new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "I", "II", "Vrtovi", "Vrtovi", "Privatna svojina", "Ostalo", "Ostalo", "ostalo", "ostalo", null, 10000, "3", "4" },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "12345435", "15a", new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "II", "II", "Njive", "Njive", "Privatna svojina", "Obradivo", "Ostalo", "ostalo", "ostalo", null, 3000, "3", "4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeloviParcele_ParcelaMParcelaId",
                table: "DeloviParcele",
                column: "ParcelaMParcelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_OpstinaId",
                table: "Parcele",
                column: "OpstinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeloviParcele");

            migrationBuilder.DropTable(
                name: "Parcele");

            migrationBuilder.DropTable(
                name: "Opstine");
        }
    }
}
