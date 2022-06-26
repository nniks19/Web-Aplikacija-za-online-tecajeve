using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Status_NarudzbeId { get; set; }
        [ValidateNever]
        public Status_narudzbe Status_narudzbe { get; set; }
        [Required(ErrorMessage = "Osoba mora biti odabrana!")]
        public string OsobaOib { get; set; }
        [ValidateNever]
        public Osoba Osoba { get; set; }
        [Required(ErrorMessage = "Tečaj mora biti odabran!")]
        public int? TecajId { get; set; }
        [ValidateNever]
        public Tecaj Tecaj { get; set; }
        [Required(ErrorMessage = "Način plaćanja mora biti odabran!")]
        public int? Nacin_PlacanjaId { get; set; }
        [ValidateNever]
        public Nacin_placanja Nacin_placanja { get; set; }
        [Required]
        public long datum_pocetak { get; set; }
        [Required]
        public long datum_zavrsetak { get; set; }
    }
}
/*
 CREATE TABLE Narudzba(
 id int NOT NULL AUTO_INCREMENT,
 status_id int,
 osoba_oib varchar(11),
 tecaj_id int,
 nacin_placanja_id int,
 datum_pocetak timestamp DEFAULT CURRENT_TIMESTAMP,
 datum_zavrsetak timestamp NULL,
 CONSTRAINT PK_Narudzba PRIMARY KEY(id),
 CONSTRAINT FK_Narudzba_status_id FOREIGN KEY(status_id) REFERENCES Status_Narudzbe(id),
 CONSTRAINT FK_Narudzba_osoba_oib FOREIGN KEY(osoba_oib) REFERENCES Osoba(oib),
 CONSTRAINT FK_Narudzba_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id),
 CONSTRAINT FK_Narudzba_nacin_placanja_id FOREIGN KEY(nacin_placanja_id) REFERENCES Nacin_Placanja(id)
);
*/