
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Razgovor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Osoba kojoj se šalje poruka mora biti odabrana!")]
        public string PrimateljOsobaOib { get; set; }
        [ValidateNever]
        public Osoba PrimateljOsoba { get; set; }
        [Required(ErrorMessage = "Osoba koja šalje poruka mora biti odabrana!")]
        public string PosiljateljOsobaOib { get; set; }
        [ValidateNever]
        public Osoba PosiljateljOsoba { get; set; }
    }
}
/*
 CREATE TABLE Status_Narudzbe(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Status_Narudzbe PRIMARY KEY(id)
);
*/