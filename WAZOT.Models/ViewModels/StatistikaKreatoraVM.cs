using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOT.Models.ViewModels
{
    public class StatistikaKreatoraVM
    {
        [ValidateNever]
        public int? brPrijava { get; set; }
        [ValidateNever]
        public int? brOcjenaTecaja { get; set; }
        [ValidateNever]
        public int? brTecaja { get; set; }
        [ValidateNever]
        public int? brVideozapisa { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? OsobaList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? TecajList { get; set; }
        [ValidateNever]
        public int? TecajId { get; set; }
        [ValidateNever]
        public string? OsobaOib { get; set; }
        [ValidateNever]
        public int? brKlikovaNaTecaju { get; set; }
        [ValidateNever]
        public int? brKlikovaNaVideozapise{ get; set; }
        [ValidateNever]
        public int? brKorisnika { get; set; }
        [ValidateNever]
        public int? brOdobrenihPrijava { get; set; }
        [ValidateNever]
        public float? prosjecnaOcjena { get; set; }
        [ValidateNever]
        public int? brCjelina { get; set; }
        [ValidateNever]
        public int? brTecajeva { get; set; }
        [ValidateNever]
        public int? najvisePregleda { get; set; }
        [ValidateNever]
        public int? najvisePosjeta { get; set; }
        [ValidateNever]
        public string? posljednjaAktivnost { get; set; }
    }
}
