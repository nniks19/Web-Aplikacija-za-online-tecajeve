using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Status_narudzbe
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Unos naziva statusa narudžbe je obavezan!")]
        public string naziv { get; set; }
    }
}
/*
 CREATE TABLE Status_Narudzbe(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Status_Narudzbe PRIMARY KEY(id)
);
*/