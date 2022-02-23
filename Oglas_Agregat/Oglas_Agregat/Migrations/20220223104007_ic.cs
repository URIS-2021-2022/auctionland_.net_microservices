using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oglas_Agregat.Migrations
{
    public partial class ic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Oglasi",
                columns: table => new
                {
                    OglasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RokZaZalbu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpisOglasa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjavljenUListuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JavnoNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglasi", x => x.OglasId);
                    table.ForeignKey(
                        name: "FK_Oglasi_SluzbeniListovi_ObjavljenUListuId",
                        column: x => x.ObjavljenUListuId,
                        principalTable: "SluzbeniListovi",
                        principalColumn: "SluzbeniListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SluzbeniListovi",
                columns: new[] { "SluzbeniListId", "BrojLista", "DatumIzdanja" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), 33, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Oglasi",
                columns: new[] { "OglasId", "DatumObjave", "JavnoNadmetanjeId", "ObjavljenUListuId", "OpisOglasa", "RokZaZalbu" },
                values: new object[] { new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1ae8137b-1674-4c91-a4b5-87a133f5dd87"), new Guid("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"), "fdafdafa", new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Oglasi_ObjavljenUListuId",
                table: "Oglasi",
                column: "ObjavljenUListuId");
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
