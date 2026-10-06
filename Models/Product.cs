using StockControl.Utils;

namespace StockControl.Models
{
    public record struct Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public ProductCategory Category { get; set; }
    }
}