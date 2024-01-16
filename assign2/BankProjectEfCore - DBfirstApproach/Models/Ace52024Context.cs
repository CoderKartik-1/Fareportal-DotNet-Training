using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankProjectEfCore.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    

    public virtual DbSet<KartikSbaccount> KartikSbaccounts { get; set; }

    public virtual DbSet<KartikSbtransaction> KartikSbtransactions { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True; encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<KartikSbaccount>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__KartikSB__BE2ACD6E20127B65");

            entity.ToTable("KartikSBAccount");

            entity.Property(e => e.AccountNumber).ValueGeneratedNever();
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<KartikSbtransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__KartikSB__55433A6BD3049A65");

            entity.ToTable("KartikSBTransaction");

            entity.Property(e => e.TransactionId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.AccountNumberNavigation).WithMany(p => p.KartikSbtransactions)
                .HasForeignKey(d => d.AccountNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KartikSBT__Accou__12FDD1B2");
        });

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
