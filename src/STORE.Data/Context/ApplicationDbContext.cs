using Microsoft.EntityFrameworkCore;
using STORE.Entities.Models;

namespace STORE.Data.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Tienda> Tiendas { get; set; }
        public virtual DbSet<ArticuloTienda> ArticulosTiendas { get; set; }
        public virtual DbSet<ClienteArticulo> ClientesArticulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
