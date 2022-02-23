using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zalba.Migrations
{
    public partial class nova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PodnosilacZalbe",
                table: "Zalbe",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PodnosilacZalbe",
                table: "Zalbe");
        }
    }
}
