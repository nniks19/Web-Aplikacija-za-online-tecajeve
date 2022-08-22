using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace WAZOT.Models
{
    public class Cjelina_tecaja
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tečaj mora biti odabran!")]
        public int? TecajId { get; set; }
        [ValidateNever]
        public Tecaj Tecaj { get; set; }
        [Required(ErrorMessage = "Naziv cjeline tečaja je obavezan!")]
        public string naziv_cjeline { get; set; }
        [Required(ErrorMessage = "Opis cjeline tečaja je obavezan!")]
        public string opis_cjeline { get; set; }
    }
}