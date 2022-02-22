using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liciter___Agregat.Migrations
{
    public partial class OvlacenoLice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KupacModelOvlascenoLiceModel");

            migrationBuilder.AddColumn<Guid>(
                name: "KupacModelKupacId",
                table: "OvlascenaLica",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OvlascenaLica_KupacModelKupacId",
                table: "OvlascenaLica",
                column: "KupacModelKupacId");

            migrationBuilder.AddForeignKey(
                name: "FK_OvlascenaLica_Kupci_KupacModelKupacId",
                table: "OvlascenaLica",
                column: "KupacModelKupacId",
                principalTable: "Kupci",
                principalColumn: "KupacId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OvlascenaLica_Kupci_KupacModelKupacId",
                table: "OvlascenaLica");

            migrationBuilder.DropIndex(
                name: "IX_OvlascenaLica_KupacModelKupacId",
                table: "OvlascenaLica");

            migrationBuilder.DropColumn(
                name: "KupacModelKupacId",
                table: "OvlascenaLica");

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

            migrationBuilder.CreateIndex(
                name: "IX_KupacModelOvlascenoLiceModel_OvlascenaLicaOvlascenoLiceId",
                table: "KupacModelOvlascenoLiceModel",
                column: "OvlascenaLicaOvlascenoLiceId");
        }
    }
}
