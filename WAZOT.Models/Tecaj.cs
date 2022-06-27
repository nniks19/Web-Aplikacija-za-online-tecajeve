using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Tecaj
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Osoba mora biti odabrana!")]
        public string OsobaOib { get; set; }
        [Required]
        public float cijena { get; set; }
        [ValidateNever]
        public Osoba Osoba { get; set; }
        [Required(ErrorMessage = "Naziv je obavezan!")]
        public string naziv { get; set; }
        [Required(ErrorMessage = "Opis je obavezan!")]
        public string opis { get; set; }
        //[Required] - dodat kasnije dok dodam ocjene
        public float prosjecna_ocjena { get; set; }
        [Required(ErrorMessage = "Kategorija mora biti odabrana!")]
        public int? KategorijaId { get; set; }
        [ValidateNever]
        public Kategorija? Kategorija { get; set; }
        [Required(ErrorMessage = "Tečaj mora imati sliku!")]
        public String slika { get; set; }

    }
}

/*
 * CREATE TABLE Tecaj(
 id int NOT NULL AUTO_INCREMENT,
 vlasnik_tecaja varchar(11),
 naziv varchar(50),
 opis TEXT,
 prosjecna_ocjena float,
 CONSTRAINT PK_Tecaj PRIMARY KEY (id),
 CONSTRAINT FK_Tecaj_vlasnik_tecaja FOREIGN KEY(vlasnik_tecaja) REFERENCES Osoba(oib)
);
*/