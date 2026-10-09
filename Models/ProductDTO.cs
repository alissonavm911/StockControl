using StockControl.Utils;

namespace StockControl.Models
{
    public struct ProductDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public uint? Quantity { get; set; }
        public EProductCategory? Category { get; set; }
    }
}