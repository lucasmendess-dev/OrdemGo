using Microsoft.EntityFrameworkCore;
using OrdemGo.Models;

namespace OrdemGo.Data
{
    public class OrdemGoContext : DbContext
    {
        public OrdemGoContext(DbContextOptions<OrdemGoContext> options)
            : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<OrdemServicoItem> OrdemServicoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EMPRESA -> USUÁRIOS
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Usuarios)
                .WithOne(u => u.Empresa)
                .HasForeignKey(u => u.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // EMPRESA -> CLIENTES
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Clientes)
                .WithOne(c => c.Empresa)
                .HasForeignKey(c => c.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // EMPRESA -> SERVIÇOS
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Servicos)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // EMPRESA -> PRODUTOS
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Produtos)
                .WithOne(p => p.Empresa)
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // EMPRESA -> ORDENS DE SERVIÇO
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.OrdensServico)
                .WithOne(o => o.Empresa)
                .HasForeignKey(o => o.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // CLIENTE -> ORDENS DE SERVIÇO
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.OrdensServico)
                .WithOne(o => o.Cliente)
                .HasForeignKey(o => o.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ORDEM DE SERVIÇO -> ITENS
            modelBuilder.Entity<OrdemServico>()
                .HasMany(o => o.Itens)
                .WithOne(i => i.OrdemServico)
                .HasForeignKey(i => i.OrdemServicoId)
                .OnDelete(DeleteBehavior.Cascade);

            // ITEM -> PRODUTO
            modelBuilder.Entity<OrdemServicoItem>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ITEM -> SERVIÇO
            modelBuilder.Entity<OrdemServicoItem>()
                .HasOne(i => i.Servico)
                .WithMany()
                .HasForeignKey(i => i.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // CNPJ ÚNICO
            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.Cnpj)
                .IsUnique();

            // E-MAIL ÚNICO POR EMPRESA
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => new
                {
                    u.EmpresaId,
                    u.Email
                })
                .IsUnique();

            // NÚMERO DA OS ÚNICO DENTRO DA EMPRESA
            modelBuilder.Entity<OrdemServico>()
                .HasIndex(o => new
                {
                    o.EmpresaId,
                    o.Numero
                })
                .IsUnique();
        }
    }
}