using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Domain.Model;

namespace suaconsulta_api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ModelDoctor> Doctor { get; set; }

        public DbSet<ModelPatient> Patient { get; set; }

        public DbSet<ModelConsultation> Consultation { get; set; }

        public DbSet<ModelDoctorSchedule> DoctorSchedule { get; set; }

        public DbSet<ModelUsers> Users { get; set; }
    }
}