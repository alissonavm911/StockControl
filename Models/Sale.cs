using System.Text.Json.Serialization;

namespace StockControl.Models
{
    public record struct Sale
    {
        [JsonInclude] public Guid Id { get; private set; } = Guid.NewGuid();
        public required DateTime Date { get; set; }
        public required HashSet<Product> Products { get; set; }
        public required List<uint> Quantities { get; set; }
        public required List<decimal> Values { get; set; }

        public Sale()
        {
        }
    }
}