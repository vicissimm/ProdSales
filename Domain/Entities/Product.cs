
namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public User User { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
