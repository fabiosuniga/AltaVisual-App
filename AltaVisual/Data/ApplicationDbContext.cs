using AltaVisual.Models; // Ajuste para o nome do seu projeto
using Microsoft.EntityFrameworkCore;

namespace AltaVisual.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // diz ao banco quais tabelas criar
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ProdutoGrafico> Produtos { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Adesivo> Adesivos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Garante que valores decimais não deem erro de precisão no SQL
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
    }
}