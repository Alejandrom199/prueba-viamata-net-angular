using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace cineManagementDatabaseFirst.Contexts;

public partial class CineManagementDbContext : DbContext
{
    public CineManagementDbContext()
    {
    }

    public CineManagementDbContext(DbContextOptions<CineManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<pelicula> peliculas { get; set; }

    public virtual DbSet<pelicula_sala_cine> pelicula_sala_cines { get; set; }

    public virtual DbSet<sala_cine> sala_cines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CineManagement;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<pelicula>(entity =>
        {
            entity.HasKey(e => e.PeliculaId).HasName("PK__pelicula__5AC6FCCC0302FBD8");

            entity.ToTable("pelicula");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.EsActivo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<pelicula_sala_cine>(entity =>
        {
            entity.HasKey(e => e.PeliculaSalaCineId).HasName("PK__pelicula__E98D940F07BFCCF3");

            entity.ToTable("pelicula_sala_cine");

            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");

            entity.HasOne(d => d.Pelicula).WithMany(p => p.pelicula_sala_cines)
                .HasForeignKey(d => d.PeliculaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pelicula___Pelic__3D5E1FD2");

            entity.HasOne(d => d.Sala).WithMany(p => p.pelicula_sala_cines)
                .HasForeignKey(d => d.SalaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pelicula___SalaI__3E52440B");
        });

        modelBuilder.Entity<sala_cine>(entity =>
        {
            entity.HasKey(e => e.SalaId).HasName("PK__sala_cin__0428487AB7B0B7B3");

            entity.ToTable("sala_cine");

            entity.Property(e => e.EsActivo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<PeliculaConSalasDTO>().HasNoKey();
        modelBuilder.Entity<PeliculaConDetallesDTO>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
