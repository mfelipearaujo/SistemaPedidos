using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapear nomes das tabelas
        modelBuilder.Entity<Cliente>().ToTable("Clientes");
        modelBuilder.Entity<Produto>().ToTable("Produtos");
        modelBuilder.Entity<Pedido>().ToTable("Pedidos");
        modelBuilder.Entity<ItemPedido>().ToTable("ItensPedido");

        // Configurar chave composta para ItemPedido
        modelBuilder.Entity<ItemPedido>()
            .HasKey(ip => new { ip.PedidoId, ip.ProdutoId });

        // Relacionamentos
        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Pedido) // Um ItemPedido tem um Pedido
            .WithMany(p => p.Itens) // Um Pedido tem muitos Itens (coleção chamada Itens)
            .HasForeignKey(ip => ip.PedidoId); // A FK está em ItemPedido.PedidoId

        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Produto) // Um ItemPedido tem um Produto
            .WithMany() // Produto não tem coleção de ItensPedido (não navegamos de Produto para ItemPedido)
            .HasForeignKey(ip => ip.ProdutoId); // A FK está em ItemPedido.ProdutoId
    }
}
