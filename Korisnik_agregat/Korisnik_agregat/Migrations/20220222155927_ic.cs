using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Korisnik_agregat.Migrations
{
    public partial class ic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipKorisnikaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "TipoviKorisnika",
                columns: table => new
                {
                    TipKorisnikaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTipa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviKorisnika", x => x.TipKorisnikaId);
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "KorisnikId", "Ime", "KorisnickoIme", "Lozinka", "Prezime", "Salt", "TipKorisnikaID" },
                values: new object[] { new Guid("34f11383-cb12-481d-9ff7-2fd458dc7e2b"), "Vlado", "Vlado", "Hp4FA+Davx87H1kjksH9KqfoQwWORWRuIIC/ORXK6bpf6L58i76CCzvr/hnhzP8cumQlxGwPdNw3T98Mi6opEdNB8YpyyQMP7I/r86lg7dw21/VWbLdBhxk8FkiQh7Zzx3ghShxochj9yvOKGcif8yiqqRcPnf8/FSNeBzyY9VierHh9SdHN4bw7kY5c5aI84yih2cJKQZXs3UHttYl/TvHDG+djhax22GZHn4joKbrxvkbi+OAZcDbekkWuK96MEziob/ROCBx7WKN8CQ+DHy50S36Zk/XZHWo+uXrUdk2xS35ii4J4Uxx5FFsof/h1KTz3ZaQpo8EHFd0H9QycrQ==", "Cetkovic", "K0mnPR8m67LE2Q==", new Guid("577a8f2b-1a55-4e91-a3ea-3d5cf16814a6") });

            migrationBuilder.InsertData(
                table: "TipoviKorisnika",
                columns: new[] { "TipKorisnikaId", "NazivTipa" },
                values: new object[,]
                {
                    { new Guid("411badd1-bee5-4bf4-9eab-138806fa8914"), "Operater" },
                    { new Guid("62d026f1-caf7-4eae-bf1a-261a95f7f0bb"), "Tehnički sekretar" },
                    { new Guid("6216bfa0-66d0-48e8-9014-ad1030f10140"), "Prva komisija" },
                    { new Guid("b02616e3-e14d-4548-ba2b-c064b6f21a98"), "Superuser" },
                    { new Guid("d7a63d05-c1f3-4936-9b70-f0b2b496d297"), "Operater nadmetanja" },
                    { new Guid("bcafc2c9-378b-42a5-8fb7-10a0c4726a22"), "Licitant" },
                    { new Guid("3634f986-0503-4369-a7ea-891517db5084"), "Menadžer" },
                    { new Guid("577a8f2b-1a55-4e91-a3ea-3d5cf16814a6"), "Administrator" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "TipoviKorisnika");
        }
    }
}
