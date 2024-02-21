using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class ProdSalesContext : DbContext, IProdSalesDataContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<Cart> Carts { get; set; }

        public ProdSalesContext(DbContextOptions<ProdSalesContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Product>()
                .HasOne(u => u.User)
                .WithMany(p => p.Products)
                .HasForeignKey(u => u.UserId);
            
            modelBuilder.Entity<Cart>()
                .Property(c => c.Id) 
                .UseIdentityColumn();

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId);

        }
    }
}
