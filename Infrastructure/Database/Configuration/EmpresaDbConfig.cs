using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class EmpresaDbConfig : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CNPJ).IsRequired();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.FaturamentoMensal).IsRequired();
            builder.Property(x => x.Ramo).IsRequired();
            
            builder.Property(x => x.Limite).IsRequired(false);

            builder.HasMany(e => e.NotasFiscais)
                   .WithOne(nf => nf.Empresa)
                   .HasForeignKey(nf => nf.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
