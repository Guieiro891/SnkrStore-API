using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Tenis> Tenis { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedidos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    
}