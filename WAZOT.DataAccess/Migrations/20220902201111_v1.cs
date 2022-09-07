using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOT.DataAccess.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Razina_Prava",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razina_Prava", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Status_Prijave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Prijave", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    Oib = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Razina_PravaId = table.Column<int>(type: "int", nullable: false),
                    ime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prezime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lozinka = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    odobreno = table.Column<int>(type: "int", nullable: false),
                    pin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.Oib);
                    table.ForeignKey(
                        name: "FK_Osoba_Razina_Prava_Razina_PravaId",
                        column: x => x.Razina_PravaId,
                        principalTable: "Razina_Prava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Razgovor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrimateljOsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PosiljateljOsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razgovor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Razgovor_Osoba_PosiljateljOsobaOib",
                        column: x => x.PosiljateljOsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Razgovor_Osoba_PrimateljOsobaOib",
                        column: x => x.PrimateljOsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tecaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    opis = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KategorijaId = table.Column<int>(type: "int", nullable: false),
                    slika = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prosjecna_ocjena = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tecaj_Kategorija_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tecaj_Osoba_OsobaOib",
                        column: x => x.OsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Poruka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tekst = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RazgovorId = table.Column<int>(type: "int", nullable: false),
                    Datum_slanja = table.Column<long>(type: "bigint", nullable: false),
                    PosiljateljOsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poruka_Osoba_PosiljateljOsobaOib",
                        column: x => x.PosiljateljOsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poruka_Razgovor_RazgovorId",
                        column: x => x.RazgovorId,
                        principalTable: "Razgovor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CjelinaTecaja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TecajId = table.Column<int>(type: "int", nullable: false),
                    naziv_cjeline = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    opis_cjeline = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CjelinaTecaja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CjelinaTecaja_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pracenje_Korisnika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TecajId = table.Column<int>(type: "int", nullable: false),
                    brPosjeta = table.Column<int>(type: "int", nullable: false),
                    Datum_posjete = table.Column<long>(type: "bigint", nullable: false),
                    brPokretanjaVideozapisa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracenje_Korisnika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracenje_Korisnika_Osoba_OsobaOib",
                        column: x => x.OsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pracenje_Korisnika_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrijavaNaTecaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status_PrijaveId = table.Column<int>(type: "int", nullable: false),
                    OsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TecajId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaNaTecaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrijavaNaTecaj_Osoba_OsobaOib",
                        column: x => x.OsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrijavaNaTecaj_Status_Prijave_Status_PrijaveId",
                        column: x => x.Status_PrijaveId,
                        principalTable: "Status_Prijave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrijavaNaTecaj_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OcjeneTecaja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TecajId = table.Column<int>(type: "int", nullable: false),
                    Cjelina_tecajaId = table.Column<int>(type: "int", nullable: false),
                    komentar = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ocjena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcjeneTecaja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcjeneTecaja_CjelinaTecaja_Cjelina_tecajaId",
                        column: x => x.Cjelina_tecajaId,
                        principalTable: "CjelinaTecaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcjeneTecaja_Osoba_OsobaOib",
                        column: x => x.OsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcjeneTecaja_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Videozapis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TecajId = table.Column<int>(type: "int", nullable: false),
                    videozapis_putanja = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    videozapis_tip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    videozapis_naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cjelina_TecajaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videozapis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videozapis_CjelinaTecaja_Cjelina_TecajaId",
                        column: x => x.Cjelina_TecajaId,
                        principalTable: "CjelinaTecaja",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Videozapis_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NeprikladniKomentar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrijavaOsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrijavljenOsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ocjena_tecajaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeprikladniKomentar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NeprikladniKomentar_OcjeneTecaja_Ocjena_tecajaId",
                        column: x => x.Ocjena_tecajaId,
                        principalTable: "OcjeneTecaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeprikladniKomentar_Osoba_PrijavaOsobaOib",
                        column: x => x.PrijavaOsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeprikladniKomentar_Osoba_PrijavljenOsobaOib",
                        column: x => x.PrijavljenOsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CjelinaTecaja_TecajId",
                table: "CjelinaTecaja",
                column: "TecajId");

            migrationBuilder.CreateIndex(
                name: "IX_NeprikladniKomentar_Ocjena_tecajaId",
                table: "NeprikladniKomentar",
                column: "Ocjena_tecajaId");

            migrationBuilder.CreateIndex(
                name: "IX_NeprikladniKomentar_PrijavaOsobaOib",
                table: "NeprikladniKomentar",
                column: "PrijavaOsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_NeprikladniKomentar_PrijavljenOsobaOib",
                table: "NeprikladniKomentar",
                column: "PrijavljenOsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_OcjeneTecaja_Cjelina_tecajaId",
                table: "OcjeneTecaja",
                column: "Cjelina_tecajaId");

            migrationBuilder.CreateIndex(
                name: "IX_OcjeneTecaja_OsobaOib",
                table: "OcjeneTecaja",
                column: "OsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_OcjeneTecaja_TecajId",
                table: "OcjeneTecaja",
                column: "TecajId");

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_Razina_PravaId",
                table: "Osoba",
                column: "Razina_PravaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_PosiljateljOsobaOib",
                table: "Poruka",
                column: "PosiljateljOsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_RazgovorId",
                table: "Poruka",
                column: "RazgovorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracenje_Korisnika_OsobaOib",
                table: "Pracenje_Korisnika",
                column: "OsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Pracenje_Korisnika_TecajId",
                table: "Pracenje_Korisnika",
                column: "TecajId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaNaTecaj_OsobaOib",
                table: "PrijavaNaTecaj",
                column: "OsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaNaTecaj_Status_PrijaveId",
                table: "PrijavaNaTecaj",
                column: "Status_PrijaveId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaNaTecaj_TecajId",
                table: "PrijavaNaTecaj",
                column: "TecajId");

            migrationBuilder.CreateIndex(
                name: "IX_Razgovor_PosiljateljOsobaOib",
                table: "Razgovor",
                column: "PosiljateljOsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Razgovor_PrimateljOsobaOib",
                table: "Razgovor",
                column: "PrimateljOsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Tecaj_KategorijaId",
                table: "Tecaj",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecaj_OsobaOib",
                table: "Tecaj",
                column: "OsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Videozapis_Cjelina_TecajaId",
                table: "Videozapis",
                column: "Cjelina_TecajaId");

            migrationBuilder.CreateIndex(
                name: "IX_Videozapis_TecajId",
                table: "Videozapis",
                column: "TecajId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NeprikladniKomentar");

            migrationBuilder.DropTable(
                name: "Poruka");

            migrationBuilder.DropTable(
                name: "Pracenje_Korisnika");

            migrationBuilder.DropTable(
                name: "PrijavaNaTecaj");

            migrationBuilder.DropTable(
                name: "Videozapis");

            migrationBuilder.DropTable(
                name: "OcjeneTecaja");

            migrationBuilder.DropTable(
                name: "Razgovor");

            migrationBuilder.DropTable(
                name: "Status_Prijave");

            migrationBuilder.DropTable(
                name: "CjelinaTecaja");

            migrationBuilder.DropTable(
                name: "Tecaj");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "Razina_Prava");
        }
    }
}
