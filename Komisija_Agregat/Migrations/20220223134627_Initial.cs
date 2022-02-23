using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Komisija_Agregat.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predsednici",
                columns: table => new
                {
                    PredsednikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImePredsednika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrezimePredsednika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailPredsednika = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predsednici", x => x.PredsednikId);
                });

            migrationBuilder.CreateTable(
                name: "Komisije",
                columns: table => new
                {
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PredsednikId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisije", x => x.KomisijaId);
                    table.ForeignKey(
                        name: "FK_Komisije_Predsednici_PredsednikId",
                        column: x => x.PredsednikId,
                        principalTable: "Predsednici",
                        principalColumn: "PredsednikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClanoviKomisije",
                columns: table => new
                {
                    ClanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeClana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrezimeClana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailClana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanoviKomisije", x => x.ClanId);
                    table.ForeignKey(
                        name: "FK_ClanoviKomisije_Komisije_KomisijaId",
                        column: x => x.KomisijaId,
                        principalTable: "Komisije",
                        principalColumn: "KomisijaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviKomisije_KomisijaId",
                table: "ClanoviKomisije",
                column: "KomisijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisije_PredsednikId",
                table: "Komisije",
                column: "PredsednikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanoviKomisije");

            migrationBuilder.DropTable(
                name: "Komisije");

            migrationBuilder.DropTable(
                name: "Predsednici");
        }
    }
}
