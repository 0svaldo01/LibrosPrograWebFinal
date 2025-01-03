using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LibrosPrograWebFinal.Models;

public partial class LibreriaprograwebContext : DbContext
{
    public LibreriaprograwebContext()
    {
    }

    public LibreriaprograwebContext(DbContextOptions<LibreriaprograwebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autores> Autores { get; set; }

    public virtual DbSet<Generosliterarios> Generosliterarios { get; set; }

    public virtual DbSet<Libros> Libros { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=libreriaprograweb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Autores>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PRIMARY");

            entity.ToTable("autores");

            entity.Property(e => e.Nacionalidad).HasMaxLength(100);
            entity.Property(e => e.NombreAutor).HasMaxLength(150);
        });

        modelBuilder.Entity<Generosliterarios>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PRIMARY");

            entity.ToTable("generosliterarios");

            entity.Property(e => e.NombreGenero).HasMaxLength(100);
        });

        modelBuilder.Entity<Libros>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PRIMARY");

            entity.ToTable("libros");

            entity.HasIndex(e => e.IdAutor, "IdAutor");

            entity.HasIndex(e => e.IdGenero, "IdGenero");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("libros_ibfk_2");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("libros_ibfk_1");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.NombreCompleto).HasMaxLength(255);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
