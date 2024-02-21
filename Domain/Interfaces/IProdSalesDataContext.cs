using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IProdSalesDataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Cart> Carts { get; set; }


    }
}
