using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Komisija_Agregat.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClanoviKomisije",
                columns: table => new
                {
                    ClanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeClana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezimeClana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailClana = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanoviKomisije", x => x.ClanId);
                });

            migrationBuilder.CreateTable(
                name: "Komisije",
                columns: table => new
                {
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Predsednik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clanovi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisije", x => x.KomisijaId);
                });

            migrationBuilder.CreateTable(
                name: "Predsednici",
                columns: table => new
                {
                    PredsednikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImePredsednika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezimePredsednika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPredsednika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predsednici", x => x.PredsednikId);
                });

            migrationBuilder.InsertData(
                table: "ClanoviKomisije",
                columns: new[] { "ClanId", "EmailClana", "ImeClana", "PrezimeClana" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "jocko@mail.com", "Nenad", "Jeckovic" });

            migrationBuilder.InsertData(
                table: "Komisije",
                columns: new[] { "KomisijaId", "Clanovi", "Predsednik" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "sdd", "sd" });

            migrationBuilder.InsertData(
                table: "Predsednici",
                columns: new[] { "PredsednikId", "EmailPredsednika", "ImePredsednika", "PrezimePredsednika" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "markuza@mail.com", "Petar", "Markovic" });
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
