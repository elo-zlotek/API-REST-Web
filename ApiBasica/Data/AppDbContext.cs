using Microsoft.EntityFrameworkCore;

namespace ApiBasica;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Produto
        var produto = modelBuilder.Entity<Produto>();

        produto
            .Property(p => p.Nome)
            .HasMaxLength(120)
            .IsRequired();

        produto
            .Property(p => p.Preco)
            .HasPrecision(18, 2)
            .IsRequired();

        produto
            .Property(p => p.Estoque)
            .IsRequired();

        // Categoria
        var categoria = modelBuilder.Entity<Categoria>();

        categoria
            .Property(c => c.Nome)
            .HasMaxLength(80)
            .IsRequired();

        categoria
            .Property(c => c.Descricao)
            .HasMaxLength(200);

        // Cliente
        var cliente = modelBuilder.Entity<Cliente>();

        cliente
            .Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        cliente
            .Property(c => c.Email)
            .IsRequired();

        cliente
            .Property(c => c.Idade)
            .IsRequired();
    }
}