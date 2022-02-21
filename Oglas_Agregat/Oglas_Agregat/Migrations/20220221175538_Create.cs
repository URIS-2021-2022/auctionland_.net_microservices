using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oglas_Agregat.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oglasi",
                columns: table => new
                {
                    OglasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RokZaZalbu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpisOglasa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjavljenUListuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglasi", x => x.OglasId);
                });

            migrationBuilder.CreateTable(
                name: "SluzbeniListovi",
                columns: table => new
                {
                    SluzbeniListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumIzdanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojLista = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SluzbeniListovi", x => x.SluzbeniListId);
                });

            migrationBuilder.InsertData(
                table: "Oglasi",
                columns: new[] { "OglasId", "DatumObjave", "ObjavljenUListuId", "OpisOglasa", "RokZaZalbu" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), "fdafdafa", new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SluzbeniListovi",
                columns: new[] { "SluzbeniListId", "BrojLista", "DatumIzdanja" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), 33, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oglasi");

            migrationBuilder.DropTable(
                name: "SluzbeniListovi");
        }
    }
}
