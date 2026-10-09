namespace StockControl.Models
{
    public record struct SaleDto
    {
        public required DateTime Date { get; set; }
        public required string[] ProductsName { get; set; }
        public required uint[] Quantities { get; set; }
    }
}