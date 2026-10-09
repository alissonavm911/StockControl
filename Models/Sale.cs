using System.Text.Json.Serialization;

namespace StockControl.Models
{
    public record struct SaleItem
    {
        public required Product Product { get; set; }
        public required uint Quantity { get; set; }
        public required decimal Value { get; set; }
    }
    
    public record struct Sale
    {
        [JsonInclude] public Guid Id { get; private set; } = Guid.NewGuid();
        public required DateTime Date { get; set; }
        public required List<SaleItem> Items { get; set; }
        public required decimal TotalValue { get; set; }

        public Sale()
        {
        }
    }
}