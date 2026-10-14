using GerenciadorProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorProdutos.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produtos");
            e.HasKey(p => p.Id);
            e.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            e.Property(p => p.Descricao).IsRequired().HasMaxLength(500);
            e.Property(p => p.Preco).HasPrecision(18, 2);
        });
    }
}
