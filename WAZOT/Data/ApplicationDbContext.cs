using Microsoft.EntityFrameworkCore;
using WAZOT.Models;

namespace WAZOT.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }
    public DbSet<Razina_Prava> Razina_Prava { get; set; }
}
