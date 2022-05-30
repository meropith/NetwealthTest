using Microsoft.EntityFrameworkCore;
using Exchage.Function.Models.DB;

namespace Exchage.Function.DAL
{
    public partial class ApiDataContext : DbContext
    {
        public ApiDataContext()
        {
        }

        public ApiDataContext(DbContextOptions<ApiDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }
        public virtual DbSet<FixerRate> FixerRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Isocode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("ISOCode");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.Property(e => e.ToUsdrate)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("ToUSDRate");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ExchangeRates)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OandaRates_Currencies");
            });

            modelBuilder.Entity<FixerRate>(entity =>
            {
                entity.Property(e => e.ToUsdrate)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("ToUSDRate");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.FixerRates)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixerRates_Currencies");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
