using Microsoft.EntityFrameworkCore;
using WAZOT.Models;

namespace WAZOT.DataAccess;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }
    public DbSet<Razina_Prava> Razina_Prava { get; set; }
    public DbSet<Status_prijave> Status_Prijave { get; set; }
    public DbSet<Osoba> Osoba { get; set; }
    public DbSet<Tecaj> Tecaj { get; set; }
    public DbSet<Videozapis> Videozapis { get; set; }
    public DbSet<Ocjena_tecaja> OcjeneTecaja { get; set; }
    public DbSet<Prijava_Na_Tecaj> PrijavaNaTecaj { get; set; }
    public DbSet<Kategorija> Kategorija { get; set; }
    public DbSet<Pracenje_Korisnika> Pracenje_Korisnika { get; set; }
    public DbSet<Cjelina_tecaja> CjelinaTecaja { get; set; }
    public DbSet<Neprikladni_komentar> NeprikladniKomentar { get; set; }
    public DbSet<Razgovor> Razgovor { get; set; }
    public DbSet<Poruka> Poruka { get; set; }



}
