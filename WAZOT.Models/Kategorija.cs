using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOT.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Unos kategorije tečaja je obavezan!")]
        public string Naziv { get; set; }
    }
}
