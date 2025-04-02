using ConFinServer.Model;
using Microsoft.EntityFrameworkCore;

namespace ConFin.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Estado> Estado { get; set; }

        public DbSet<CidadeModel> Cidade { get; set; }
    }
}
