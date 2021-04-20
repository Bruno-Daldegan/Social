using Social.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Model.Modelos;

namespace Social.DAL.Contas
{
    internal class TransferenciaConfiguration : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder
                .Property(l => l.Id)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(l => l.ContaDebito)
                .HasColumnType("varchar(5)");


            builder
                .Property(l => l.ContaCredito)
                .HasColumnType("varchar(5)");

            builder
                .Property(l => l.Operacao)
                .HasColumnType("varchar(1)")
                .IsRequired();

            builder
                .Property(l => l.Valor)
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            builder
                .Property(l => l.DataOperacao)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}