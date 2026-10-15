namespace StockControl.Models
{
    public sealed record ProductSalesSummary(
        string Name,
        ulong QuantitySold,
        decimal TotalValue);

    public sealed record FinancialReport(
        IReadOnlyList<Sale> Sales,
        decimal TotalRevenue,
        ulong UnitsSold,
        IReadOnlyList<string> RestockProductNames,
        IReadOnlyList<ProductSalesSummary> TopProducts,
        IReadOnlyList<ProductSalesSummary> RevenueByCategory)
    {
        public int SaleCount => Sales.Count;

        public decimal AverageSaleValue => SaleCount == 0 ? 0m : TotalRevenue / SaleCount;

        public DateOnly? FirstSaleDate => Sales.Count == 0
            ? null
            : Sales.Min(sale => sale.Date);

        public DateOnly? LastSaleDate => Sales.Count == 0
            ? null
            : Sales.Max(sale => sale.Date);
    }
}
