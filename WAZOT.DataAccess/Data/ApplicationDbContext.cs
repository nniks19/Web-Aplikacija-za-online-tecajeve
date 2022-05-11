using Microsoft.EntityFrameworkCore;
using WAZOT.Models;

namespace WAZOT.DataAccess;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }
    public DbSet<Razina_Prava> Razina_Prava { get; set; }
    public DbSet<Status_narudzbe> Status_Narudzbe { get; set; }
    public DbSet<Nacin_placanja> NacinPlacanja { get; set; }
    public DbSet<Osoba> Osoba { get; set; }
    public DbSet<Tecaj> Tecaj { get; set; }
    public DbSet<Videozapis> Videozapis { get; set; }
    public DbSet<Ocjena_tecaja> OcjeneTecaja { get; set; }
    public DbSet<Narudzba> Narudzba { get; set; }
}
