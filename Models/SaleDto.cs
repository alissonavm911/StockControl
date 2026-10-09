namespace StockControl.Models
{
    public record struct SaleDto()
    {
        public required DateOnly Date { get; set; }
        public required Dictionary<string, uint> Products { get; set; } = new();
    }
}