using Social.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Model.Modelos;

namespace Social.DAL.Contas
{
    internal class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder
                .Property(l => l.Id)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(l => l.ContaId)
                .HasColumnType("varchar(5)")
                .IsRequired();

            builder
                .Property(l => l.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(l => l.Descricao)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(l => l.Saldo)
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            builder
                .Property(l => l.Status)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}