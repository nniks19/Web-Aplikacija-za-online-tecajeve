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
    public class NarudzbaVM
    {
        public Narudzba? Narudzba { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? TecajList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? OsobaList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? StatusNarudzbeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? NacinPlacanjaList { get; set; }
    }
}
