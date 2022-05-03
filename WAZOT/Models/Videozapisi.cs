using System.ComponentModel.DataAnnotations;

namespace WAZOT.Models
{
    public class Videozapisi
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int tecaj_id { get; set; }
        [Required]
        public string videozapis_putanja { get; set; }
        [Required]
        public string videozapis_tip { get; set; }
    }
}

/*
 CREATE TABLE Videozapisi(
 id int NOT NULL AUTO_INCREMENT,
 tecaj_id int,
 videozapis_putanja TEXT,
 videozapis_tip varchar(3),
 CONSTRAINT PK_Videozapisi PRIMARY KEY(id),
 CONSTRAINT FK_Videozapisi_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id)
);
*/