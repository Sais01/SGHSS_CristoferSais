using Microsoft.EntityFrameworkCore;
using SGHSS_CristoferSais.Models;

namespace SGHSS_CristoferSais.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
    }
}
