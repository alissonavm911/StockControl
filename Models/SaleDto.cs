namespace StockControl.Models
{
    public record struct SaleDto()
    {
        public required DateTime Date { get; set; } = new();
        public required Dictionary<string, uint> Products { get; set; } = new();
    }
}