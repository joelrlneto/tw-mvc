using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Entities;
using WebApplication4.Models.EntityConfigurations;

namespace WebApplication4.Models.Contexts
{
    public class SisMedContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SisMedContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #nullable disable
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<InformacoesComplementaresPaciente> InformacoesComplementaresPaciente { get; set; }
        public DbSet<MonitoramentoPaciente> MonitoramentosPaciente { get; set; }
        #nullable restore

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SisMed"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaConfiguration());
            modelBuilder.ApplyConfiguration(new InformacoesComplementaresPacienteConfiguration());
            modelBuilder.ApplyConfiguration(new MonitoramentoPacienteConfiguration());
        }
    }
}
