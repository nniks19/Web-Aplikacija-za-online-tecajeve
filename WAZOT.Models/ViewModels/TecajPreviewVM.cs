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
    public class TecajPreviewVM
    {
        public Tecaj? Tecaj { get; set; }
        public Ocjena_tecaja? Ocjena_Tecaja { get; set; }
        [ValidateNever]
        public IEnumerable<Videozapis>? VideozapisList { get; set; }
        [ValidateNever]
        public IEnumerable<Ocjena_tecaja>? OcjenaTecajaList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? KategorijaList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? OsobaList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CjelinaList { get; set; }
        [ValidateNever]
        public IEnumerable<Cjelina_tecaja>? CjelinaTecajaList { get; set; }
        [ValidateNever]
        public string? oibosobe { get; set; }
    }
}
