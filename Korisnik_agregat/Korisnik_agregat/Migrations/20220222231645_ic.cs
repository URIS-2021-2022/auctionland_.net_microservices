using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Korisnik_agregat.Migrations
{
    public partial class ic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    TipKorisnikaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Korisnici_TipoviKorisnika_TipKorisnikaId",
                        column: x => x.TipKorisnikaId,
                        principalTable: "TipoviKorisnika",
                        principalColumn: "TipKorisnikaId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "KorisnikId", "Ime", "KorisnickoIme", "Lozinka", "Prezime", "Salt", "TipKorisnikaId" },
                values: new object[] { new Guid("34f11383-cb12-481d-9ff7-2fd458dc7e2b"), "Vlado", "Vlado", "8/JNvMljilVshFqar8lp/rIrunkDe4CgnBjq6mIYyyvBDZZheUgYfByVv5CHudsuOwqEf5YkFkLrXHLYx1Nz/WhlwPC1xlYDB+mZMhT3PzMhPzo+X6X4gcRZ7/1lWduGDAw95fAyBhLoW6dcVDn6uqytY9U/l86kz0ECWKNLLHRasIa6nOf+Ksy5nW6y8Xx+CsdUn9/dmxOt8mSEM7UPIY/xwZWTAyUjVfNO48dT2jXdlsY+wfUiaFXAT8s/KbXr+t9EALXYORtzH6LDRb9VCCa4yYEWLQ5ttN6kIJR1XaNlj0xzXrErlpWKR+aR2v4yMOfjhHUPg1rE3gF17vrw9g==", "Cetkovic", "8UOUiiNvsZHZ+Q==", new Guid("577a8f2b-1a55-4e91-a3ea-3d5cf16814a6") });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_TipKorisnikaId",
                table: "Korisnici",
                column: "TipKorisnikaId");
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
