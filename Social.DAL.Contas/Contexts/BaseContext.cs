using Microsoft.EntityFrameworkCore;
using Social.Model.Modelos;

namespace Social.DAL.Contas
{
    public class SocialContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }

        public SocialContext(DbContextOptions<SocialContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Conta>(new ContaConfiguration());
            modelBuilder.ApplyConfiguration<Transferencia>(new TransferenciaConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) 
                return;

            optionsBuilder.UseSqlServer("Server=localhost;Database=SOCIAL;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
