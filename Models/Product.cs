using StockControl.Utils;

namespace StockControl.Models
{
    public struct Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }
    }
}