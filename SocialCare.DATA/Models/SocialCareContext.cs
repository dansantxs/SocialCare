using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialCare.DATA.Models;

public partial class SocialCareContext : DbContext
{
    public SocialCareContext()
    {
    }

    public SocialCareContext(DbContextOptions<SocialCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compras> Compras { get; set; }

    public virtual DbSet<ContasPagar> ContasPagar { get; set; }

    public virtual DbSet<ItensCompra> ItensCompra { get; set; }

    public virtual DbSet<Pessoas> Pessoas { get; set; }

    public virtual DbSet<PessoasFisicas> PessoasFisicas { get; set; }

    public virtual DbSet<PessoasJuridicas> PessoasJuridicas { get; set; }

    public virtual DbSet<Produtos> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DANIEL;Initial Catalog=SocialCare;Persist Security Info=True;User ID=sa;Password=1928;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compras>(entity =>
        {
            entity.HasOne(d => d.Pessoa).WithMany(p => p.Compras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Pessoas");
        });

        modelBuilder.Entity<ContasPagar>(entity =>
        {
            entity.HasOne(d => d.Pessoa).WithMany(p => p.ContasPagar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContasPagar_Pessoas");
        });

        modelBuilder.Entity<ItensCompra>(entity =>
        {
            entity.Property(e => e.Subtotal).HasComputedColumnSql("([quantidade]*[precoUnitario])", false);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.ItensCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItensCompra_Compras");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.ItensCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItensCompra_Produtos");
        });

        modelBuilder.Entity<Pessoas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pessoas__3213E83FB9A6CEEB");

            entity.Property(e => e.Tipo).IsFixedLength();
        });

        modelBuilder.Entity<PessoasFisicas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pessoas___3213E83F4D2B7628");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cpf).IsFixedLength();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PessoasFisicas).HasConstraintName("fk_pessoas_fisicas");
        });

        modelBuilder.Entity<PessoasJuridicas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pessoas___3213E83F4C5F87AA");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cnpj).IsFixedLength();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PessoasJuridicas).HasConstraintName("fk_pessoas_juridicas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}