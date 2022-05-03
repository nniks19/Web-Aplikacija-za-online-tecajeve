using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Ocjene_tecaja
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string osoba_oib { get; set; }
        [Required]
        public int tecaj_id { get; set; }
        [Required]
        public string komentar { get; set; }
        [Required]
        public int ocjena { get; set; }
    }
}
/*
 CREATE TABLE Ocjene_Tecaja(
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