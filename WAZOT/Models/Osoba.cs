using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Osoba
    {
        [Key]
        public string Oib { get; set; }
        [Required]
        public int id_rp { get; set; }
        [Required]
        public string ime { get; set; }
        [Required]
        public string prezime { get; set; }
        [Required]
        public string email { get; set; }
    }
}

/*
 oib varchar(11) NOT NULL,
 id_rp int NOT NULL,
 ime varchar(30),
 prezime varchar(30),
 email varchar(30),
 CONSTRAINT PK_Osoba PRIMARY KEY (oib),
 CONSTRAINT FK_Osoba_id_rp FOREIGN KEY(id_rp) REFERENCES Razina_Prava(id)
*/