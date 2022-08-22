using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Osoba
    {
        [Key]
        [Required(ErrorMessage = "Unos OIB-a je obavezan!")]
        [StringLength(11, ErrorMessage = "OIB mora imati {1} znamenki!")]
        public string? Oib { get; set; }
        [Required(ErrorMessage = "Razina prava mora biti odabrana!")]
        public int? Razina_PravaId { get; set; }
        [ValidateNever]
        public Razina_Prava? Razina_Prava { get; set; }
        [Required(ErrorMessage = "Unos imena je obavezan!")]
        public string? ime { get; set; }
        [Required(ErrorMessage = "Unos prezimena je obavezan!")]
        public string? prezime { get; set; }
        [Required(ErrorMessage = "Unos emaila je obavezan!")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Unos lozinke je obavezan!")]
        public string? lozinka { get; set; }
        [Required(ErrorMessage = "Korisnik mora biti odobren!")]
        public int? odobreno { get; set; }
        [Required(ErrorMessage = "Unos PIN-a je obavezan!")]
        public string? pin { get; set; }
    }
}

/*
 oib varchar(11) NOT NULL,
 id_rp int NOT NULL,
 ime varchar(30),
 prezime varchar(30),
 email varchar(30),
 lozinka varchar(255),
 CONSTRAINT PK_Osoba PRIMARY KEY (oib),
 CONSTRAINT FK_Osoba_id_rp FOREIGN KEY(id_rp) REFERENCES Razina_Prava(id)
*/