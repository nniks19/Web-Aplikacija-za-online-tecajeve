using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Razina_Prava
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }

    }
}
