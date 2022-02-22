using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OdlukaODavanjuUZakup.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OdlukaoDavanjuuZakup",
                columns: table => new
                {
                    OdlukaoDavanjuuZakupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    datum_donosenja_odluke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    validnost = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdlukaoDavanjuuZakup", x => x.OdlukaoDavanjuuZakupID);
                });

            migrationBuilder.CreateTable(
                name: "UgovoroZakupu",
                columns: table => new
                {
                    UgovoroZakupuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Javno_Nadmetanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    odluka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tip_garancije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rokovi_dospeca = table.Column<DateTime>(type: "datetime2", nullable: false),
                    zavodni_Broj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datum_zavodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rok_za_vracanje_zemljista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mesto_potpisivanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datum_potpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdlukaoDavanjuuZakupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UgovoroZakupu", x => x.UgovoroZakupuID);
                    table.ForeignKey(
                        name: "FK_UgovoroZakupu_OdlukaoDavanjuuZakup_OdlukaoDavanjuuZakupID",
                        column: x => x.OdlukaoDavanjuuZakupID,
                        principalTable: "OdlukaoDavanjuuZakup",
                        principalColumn: "OdlukaoDavanjuuZakupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarantPlacanja",
                columns: table => new
                {
                    GarantPlacanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opis_garanta1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis_garanta2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UgovorOZakupuID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarantPlacanja", x => x.GarantPlacanjaID);
                    table.ForeignKey(
                        name: "FK_GarantPlacanja_UgovoroZakupu_UgovorOZakupuID",
                        column: x => x.UgovorOZakupuID,
                        principalTable: "UgovoroZakupu",
                        principalColumn: "UgovoroZakupuID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UplataZakupnine",
                columns: table => new
                {
                    UplataZakupnineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    broj_racuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    poziv_na_broj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    iznos = table.Column<double>(type: "float", nullable: false),
                    svrha_uplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    javno_nadmetanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uplatilac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UgovorOZakupuID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UplataZakupnine", x => x.UplataZakupnineID);
                    table.ForeignKey(
                        name: "FK_UplataZakupnine_UgovoroZakupu_UgovorOZakupuID",
                        column: x => x.UgovorOZakupuID,
                        principalTable: "UgovoroZakupu",
                        principalColumn: "UgovoroZakupuID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GarantPlacanja",
                columns: new[] { "GarantPlacanjaID", "Opis_garanta1", "Opis_garanta2", "UgovorOZakupuID" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), "Jemstvo", "Jemstvo", null });

            migrationBuilder.InsertData(
                table: "OdlukaoDavanjuuZakup",
                columns: new[] { "OdlukaoDavanjuuZakupID", "datum_donosenja_odluke", "validnost" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true });

            migrationBuilder.InsertData(
                table: "UgovoroZakupu",
                columns: new[] { "UgovoroZakupuID", "Javno_Nadmetanje", "OdlukaoDavanjuuZakupID", "datum_potpisa", "datum_zavodjenja", "lice", "mesto_potpisivanja", "odluka", "rok_za_vracanje_zemljista", "rokovi_dospeca", "tip_garancije", "zavodni_Broj" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), null, null, new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "VrbaS", "test", new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jemstvo", "test" });

            migrationBuilder.InsertData(
                table: "UplataZakupnine",
                columns: new[] { "UplataZakupnineID", "UgovorOZakupuID", "broj_racuna", "datum", "iznos", "javno_nadmetanje", "poziv_na_broj", "svrha_uplate", "uplatilac" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), null, "11222333444", new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000.0, "javno", "15000", "svrha", "uplatilac" });

            migrationBuilder.CreateIndex(
                name: "IX_GarantPlacanja_UgovorOZakupuID",
                table: "GarantPlacanja",
                column: "UgovorOZakupuID");

            migrationBuilder.CreateIndex(
                name: "IX_UgovoroZakupu_OdlukaoDavanjuuZakupID",
                table: "UgovoroZakupu",
                column: "OdlukaoDavanjuuZakupID");

            migrationBuilder.CreateIndex(
                name: "IX_UplataZakupnine_UgovorOZakupuID",
                table: "UplataZakupnine",
                column: "UgovorOZakupuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarantPlacanja");

            migrationBuilder.DropTable(
                name: "UplataZakupnine");

            migrationBuilder.DropTable(
                name: "UgovoroZakupu");

            migrationBuilder.DropTable(
                name: "OdlukaoDavanjuuZakup");
        }
    }
}
