using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace WAZOT.Models
{
    public class Ocjena_tecaja
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Osoba mora biti odabrana!")]
        public string OsobaOib { get; set; }
        [ValidateNever]
        public Osoba Osoba { get; set; }
        [Required(ErrorMessage = "Tečaj mora biti odabran!")]
        public int? TecajId { get; set; }
        [ValidateNever]
        public Tecaj Tecaj { get; set; }
        [Required(ErrorMessage = "Cjelina tečaja mora biti odabrana!")]
        public int? Cjelina_tecajaId { get; set; }
        [ValidateNever]
        public Cjelina_tecaja Cjelina_tecaja { get; set; }
        [Required(ErrorMessage = "Komentar je obavezan!")]
        public string komentar { get; set; }
        [Required(ErrorMessage = "Ocjena je obavezna!")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti odabrana!")]
        public int ocjena { get; set; }
    }
}
/*
 CREATE TABLE Ocjena_Tecaja(
 id int NOT NULL AUTO_INCREMENT,
 osoba_oib varchar(11),
 tecaj_id int,
 komentar TEXT,
 ocjena int,
 CONSTRAINT PK_Ocjene_Tecaja PRIMARY KEY(id),
 CONSTRAINT FK_Ocjene_Tecaja_osoba_oib FOREIGN KEY(osoba_oib) REFERENCES Osoba(oib),
 CONSTRAINT FK_Ocjene_Tecaja_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id)
);
*/