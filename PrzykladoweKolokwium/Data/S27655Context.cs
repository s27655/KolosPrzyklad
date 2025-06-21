using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrzykładoweKolokwium.Models;

namespace PrzykładoweKolokwium.Data;

public partial class S27655Context : DbContext
{
    public S27655Context()
    {
    }

    public S27655Context(DbContextOptions<S27655Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Partium> Partia { get; set; }

    public virtual DbSet<Polityk> Polityks { get; set; }

    public virtual DbSet<Przynaleznosc> Przynaleznoscs { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Partia_pk");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataZalozenia).HasColumnType("datetime");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Skrot)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Polityk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Polityk_pk");

            entity.ToTable("Polityk");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Imie)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Powiedzenie)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Przynaleznosc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Przynaleznosc_pk");

            entity.ToTable("Przynaleznosc");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Do).HasColumnType("datetime");
            entity.Property(e => e.Od).HasColumnType("datetime");
            entity.Property(e => e.PartiaId).HasColumnName("Partia_ID");
            entity.Property(e => e.PolitykId).HasColumnName("Polityk_ID");

            entity.HasOne(d => d.Partia).WithMany(p => p.Przynaleznoscs)
                .HasForeignKey(d => d.PartiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Przynaleznosc_Partia");

            entity.HasOne(d => d.Polityk).WithMany(p => p.Przynaleznoscs)
                .HasForeignKey(d => d.PolitykId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Przynaleznosc_Polityk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
