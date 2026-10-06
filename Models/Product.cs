using StockControl.Utils;

namespace StockControl.Models
{
    public record struct Product
    {
        public Guid Id { get; } = Guid.NewGuid();
        public required string? Name { get; set; }
        public required decimal Price { get; set; }
        public required uint Quantity { get; set; }
        public required ProductCategory Category { get; set; }
        
        public Product()
        {
        }
    }
}