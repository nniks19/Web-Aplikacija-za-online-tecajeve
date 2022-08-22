using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOT.Models
{
    public class Poruka
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Unos teksta poruke je obavezan!")]
        public string Tekst { get; set; }
        [Required(ErrorMessage = "Razgovor mora biti odabran!")]
        public int RazgovorId { get; set; }
        [ValidateNever]
        public Razgovor Razgovor { get; set; }
        public long Datum_slanja { get; set; }
    }
}
