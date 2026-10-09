using System.Text.Json.Serialization;
using StockControl.Utils;

namespace StockControl.Models
{
    public record struct Product
    {
        [JsonInclude] public Guid Id { get; private set; } = Guid.NewGuid();
        public required string? Name { get; set; }
        public required decimal Price { get; set; }
        public required uint Quantity { get; set; }
        public required EProductCategory Category { get; set; }

        public Product()
        {
        }
    }
}