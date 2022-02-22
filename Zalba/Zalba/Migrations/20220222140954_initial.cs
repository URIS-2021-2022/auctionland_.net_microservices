using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zalba.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoviZalbi",
                columns: table => new
                {
                    TipZalbeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTipa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviZalbi", x => x.TipZalbeId);
                });

            migrationBuilder.CreateTable(
                name: "Zalbe",
                columns: table => new
                {
                    ZalbaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumPodnosenjaZalbe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RazlogZalbe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obrazlozenje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumResenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojResenja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusZalbe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojOdluke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RadnjaNaOsnovuZalbe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipZalbeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalbe", x => x.ZalbaId);
                    table.ForeignKey(
                        name: "FK_Zalbe_TipoviZalbi_TipZalbeId",
                        column: x => x.TipZalbeId,
                        principalTable: "TipoviZalbi",
                        principalColumn: "TipZalbeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TipoviZalbi",
                columns: new[] { "TipZalbeId", "NazivTipa" },
                values: new object[,]
                {
                    { new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "Žalba na tok javnog nadmetanaja" },
                    { new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "Žalba na Odluku o davanju u zakup" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "Žalba na Odluku o davanju na korišćenje" }
                });

            migrationBuilder.InsertData(
                table: "Zalbe",
                columns: new[] { "ZalbaId", "BrojOdluke", "BrojResenja", "DatumPodnosenjaZalbe", "DatumResenja", "Obrazlozenje", "RadnjaNaOsnovuZalbe", "RazlogZalbe", "StatusZalbe", "TipId", "TipZalbeId" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "S001", "S123", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje", "JN ne ide u drugi krug", "razlog", "Otvorena", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), null },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "S002", "S124", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje", "JN ide u drugi krug sa novim uslovima", "razlog", "Odbijena", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zalbe_TipZalbeId",
                table: "Zalbe",
                column: "TipZalbeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zalbe");

            migrationBuilder.DropTable(
                name: "TipoviZalbi");
        }
    }
}
