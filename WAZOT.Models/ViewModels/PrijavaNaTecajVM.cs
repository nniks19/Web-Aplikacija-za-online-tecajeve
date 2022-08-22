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
    public class PrijavaNaTecajVM
    {
        public Prijava_Na_Tecaj? PrijavaNaTecaj { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? TecajList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? OsobaList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? StatusPrijavaList { get; set; }
    }
}
