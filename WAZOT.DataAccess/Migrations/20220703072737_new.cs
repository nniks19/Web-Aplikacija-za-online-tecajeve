using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOT.DataAccess.Migrations
{
    public partial class @new : Migration
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
                name: "NacinPlacanja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.Id);
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
                name: "Status_Narudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Narudzbe", x => x.Id);
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
                name: "Tecaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cijena = table.Column<float>(type: "float", nullable: false),
                    naziv = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    opis = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KategorijaId = table.Column<int>(type: "int", nullable: false),
                    slika = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                name: "Narudzba",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status_NarudzbeId = table.Column<int>(type: "int", nullable: false),
                    OsobaOib = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TecajId = table.Column<int>(type: "int", nullable: false),
                    Nacin_PlacanjaId = table.Column<int>(type: "int", nullable: false),
                    datum_pocetak = table.Column<long>(type: "bigint", nullable: false),
                    datum_zavrsetak = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Narudzba_NacinPlacanja_Nacin_PlacanjaId",
                        column: x => x.Nacin_PlacanjaId,
                        principalTable: "NacinPlacanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzba_Osoba_OsobaOib",
                        column: x => x.OsobaOib,
                        principalTable: "Osoba",
                        principalColumn: "Oib",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzba_Status_Narudzbe_Status_NarudzbeId",
                        column: x => x.Status_NarudzbeId,
                        principalTable: "Status_Narudzbe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzba_Tecaj_TecajId",
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
                    komentar = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ocjena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcjeneTecaja", x => x.Id);
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
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videozapis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videozapis_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_Nacin_PlacanjaId",
                table: "Narudzba",
                column: "Nacin_PlacanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_OsobaOib",
                table: "Narudzba",
                column: "OsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_Status_NarudzbeId",
                table: "Narudzba",
                column: "Status_NarudzbeId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_TecajId",
                table: "Narudzba",
                column: "TecajId");

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
                name: "IX_Tecaj_KategorijaId",
                table: "Tecaj",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecaj_OsobaOib",
                table: "Tecaj",
                column: "OsobaOib");

            migrationBuilder.CreateIndex(
                name: "IX_Videozapis_TecajId",
                table: "Videozapis",
                column: "TecajId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "OcjeneTecaja");

            migrationBuilder.DropTable(
                name: "Videozapis");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropTable(
                name: "Status_Narudzbe");

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
