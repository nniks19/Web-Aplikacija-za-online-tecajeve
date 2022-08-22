using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Pracenje_Korisnika
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
        public Tecaj? Tecaj { get; set; }
        public long Datum_posjete { get; set; }
        public long Vrijeme_videozapis { get; set; }

    }
}

/*
 CREATE TABLE Nacin_Placanja(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Nacin_Placanja PRIMARY KEY(id)
);
*/