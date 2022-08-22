using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Videozapis
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tečaj mora biti odabran!")]
        public int? TecajId { get; set; }
        [ValidateNever]
        public Tecaj Tecaj { get; set; }
        [Required(ErrorMessage = "Videozapis mora biti prenesen!")]
        public string videozapis_putanja { get; set; }
        [Required]
        public string videozapis_tip { get; set; }
        [Required(ErrorMessage = "Videozapis mora imati naziv!")]
        public string videozapis_naziv { get; set; }
        public int? CjelinaTecajaId { get; set; }
        [ValidateNever]
        public Cjelina_tecaja CjelinaTecaja { get; set; }
    }
}

/*
 CREATE TABLE Videozapis(
 id int NOT NULL AUTO_INCREMENT,
 tecaj_id int,
 videozapis_putanja TEXT,
 videozapis_tip varchar(3),
 CONSTRAINT PK_Videozapisi PRIMARY KEY(id),
 CONSTRAINT FK_Videozapisi_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id)
);
*/