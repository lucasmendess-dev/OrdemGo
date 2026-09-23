using Microsoft.EntityFrameworkCore;
using OrdemGo.Models;

namespace OrdemGo.Data;

public class OrdemGoContext : DbContext
{
    public OrdemGoContext(DbContextOptions<OrdemGoContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<OrdemServico> OrdensServico => Set<OrdemServico>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrdemServico>()
            .Property(ordem => ordem.NumeroOS)
            .HasComputedColumnSql(
                "('#OS-' + REPLICATE('0', CASE WHEN LEN(CONVERT(varchar(20), [Id])) < 5 THEN 5 - LEN(CONVERT(varchar(20), [Id])) ELSE 0 END) + CONVERT(varchar(20), [Id]))",
                stored: true);

        modelBuilder.Entity<OrdemServico>()
            .HasIndex(ordem => ordem.NumeroOS)
            .IsUnique();

        modelBuilder.Entity<OrdemServico>()
            .Property(ordem => ordem.ValorServico)
            .HasDefaultValue(0m);

        modelBuilder.Entity<OrdemServico>()
            .Property(ordem => ordem.ValorPecas)
            .HasDefaultValue(0m);

        modelBuilder.Entity<OrdemServico>()
            .HasOne(ordem => ordem.Cliente)
            .WithMany()
            .HasForeignKey(ordem => ordem.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
