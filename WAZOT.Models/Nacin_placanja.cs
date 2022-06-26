using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Nacin_placanja
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Unos naziva načina plaćanja je obavezan!")]
        public string naziv { get; set; }
    }
}

/*
 CREATE TABLE Nacin_Placanja(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Nacin_Placanja PRIMARY KEY(id)
);
*/