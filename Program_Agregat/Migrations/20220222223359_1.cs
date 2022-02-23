using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Program_Agregat.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Programi",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaksimalnoOgranicenje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Licitacije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programi", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "PredloziPlana",
                columns: table => new
                {
                    PredlogPlanaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojDokumenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MisljenjeKomisije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usvojen = table.Column<bool>(type: "bit", nullable: false),
                    DatumDokumenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgramPlanaProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredloziPlana", x => x.PredlogPlanaId);
                    table.ForeignKey(
                        name: "FK_PredloziPlana_Programi_ProgramPlanaProgramId",
                        column: x => x.ProgramPlanaProgramId,
                        principalTable: "Programi",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredloziPlana_ProgramPlanaProgramId",
                table: "PredloziPlana",
                column: "ProgramPlanaProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredloziPlana");

            migrationBuilder.DropTable(
                name: "Programi");
        }
    }
}
