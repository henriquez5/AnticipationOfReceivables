using Domain.Entities;
using Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaDbConfig());
            modelBuilder.ApplyConfiguration(new NotaFiscalDbConfig());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Empresa> Empresa { get; set; }

        public DbSet<NotaFiscal> NotaFiscal { get; set; }
    }
}
