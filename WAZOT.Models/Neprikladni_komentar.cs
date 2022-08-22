
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Neprikladni_komentar
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Korisnik koji prijavljuje mora biti odabran!")]
        public string PrijavljujeOsobaOib { get; set; }
        [ValidateNever]
        public Osoba PrijavaOsoba { get; set; }
        [Required(ErrorMessage = "Korisnik koji je napisao komentar mora biti odabran!")]
        public string PrijavljenOsobaOib { get; set; }
        [ValidateNever]
        public Osoba PrijavljenOsoba { get; set; }
        [Required(ErrorMessage = "Komentar  koji se prijavljuje mora biti odabran!")]
        public int OcjenaId { get; set; }
        [ValidateNever]
        public Ocjena_tecaja Ocjena_tecaja { get; set; }
    }
}
/*
 CREATE TABLE Status_Narudzbe(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Status_Narudzbe PRIMARY KEY(id)
);
*/