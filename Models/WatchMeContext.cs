using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WatchMe.Models;

public partial class WatchMeContext : DbContext
{
    public WatchMeContext()
    {
    }

    public WatchMeContext(DbContextOptions<WatchMeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Peliculas> Peliculas { get; set; }

    public virtual DbSet<Reseñas> Reseñas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Vermastarde> Vermastarde { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=WatchMe", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Peliculas>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PRIMARY");

            entity.ToTable("peliculas");

            entity.Property(e => e.FechaLanzamiento).HasColumnType("datetime");
            entity.Property(e => e.Genero).HasMaxLength(35);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Plataformas).HasColumnType("text");
            entity.Property(e => e.Puntuacion).HasDefaultValueSql("'0'");
            entity.Property(e => e.Sinopsis).HasMaxLength(350);
            entity.Property(e => e.Urlposter)
                .HasColumnType("text")
                .HasColumnName("URLPoster");
        });

        modelBuilder.Entity<Reseñas>(entity =>
        {
            entity.HasKey(e => e.IdReseña).HasName("PRIMARY");

            entity.ToTable("reseñas");

            entity.HasIndex(e => e.IdProduccion, "IdProduccion");

            entity.HasIndex(e => e.IdUsuario, "IdUsuario");

            entity.Property(e => e.Reseña).HasMaxLength(250);

            entity.HasOne(d => d.IdProduccionNavigation).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.IdProduccion)
                .HasConstraintName("reseñas_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reseñas_ibfk_1");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Contraseña).HasMaxLength(256);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(60);
            entity.Property(e => e.NombreUsuario).HasMaxLength(25);
            entity.Property(e => e.Rol)
                .HasMaxLength(15)
                .HasDefaultValueSql("'Usuario'");
        });

        modelBuilder.Entity<Vermastarde>(entity =>
        {
            entity.HasKey(e => e.IdVerMasTarde).HasName("PRIMARY");

            entity.ToTable("vermastarde");

            entity.HasIndex(e => e.IdProduccion, "IdProduccion");

            entity.HasIndex(e => e.IdUsuario, "IdUsuario");

            entity.HasOne(d => d.IdProduccionNavigation).WithMany(p => p.Vermastarde)
                .HasForeignKey(d => d.IdProduccion)
                .HasConstraintName("vermastarde_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Vermastarde)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vermastarde_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
