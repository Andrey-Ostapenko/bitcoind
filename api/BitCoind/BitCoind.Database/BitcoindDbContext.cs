using System;
using BitCoind.Core.Logic.Enums;
using BitCoind.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitCoind.Database
{
    public class BitcoindDbContext : DbContext
    {
        public BitcoindDbContext(DbContextOptions<BitcoindDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransactionEntity> Transactions { get; set; }

        public virtual DbSet<WalletEntity> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<TransactionEntity>(entity =>
            {
                entity.HasIndex(e => e.IdempotencyKey)
                    .HasName("UQ__Transact__A6D161D86427029D")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.IsNew)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (TransactionType)Enum.Parse(typeof(TransactionType), v));

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK_Transactions_Wallet");
            });

            modelBuilder.Entity<WalletEntity>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Balance).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120);
            });
        }
    }
}