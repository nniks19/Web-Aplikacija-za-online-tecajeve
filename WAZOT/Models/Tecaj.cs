using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Tecaj
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string vlasnik_tecaja { get; set; }
        [Required]
        public string naziv { get; set; }
        [Required]
        public string opis { get; set; }
        [Required]
        public float prosjecna_ocjena { get; set; }
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