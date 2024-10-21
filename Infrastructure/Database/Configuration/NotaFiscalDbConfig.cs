using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class NotaFiscalDbConfig : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Numero).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.DataVencimento).IsRequired();

            builder.HasOne(nf => nf.Empresa)
                   .WithMany(e => e.NotasFiscais)
                   .HasForeignKey(nf => nf.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
