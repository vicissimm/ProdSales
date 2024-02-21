
namespace Application.Dto
{
    public class ProductDto
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
