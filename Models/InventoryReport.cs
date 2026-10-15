using StockControl.Utils;

namespace StockControl.Models
{
    public sealed record CategoryInventorySummary(
        EProductCategory Category,
        int ProductCount,
        ulong TotalQuantity,
        decimal TotalValue);

    public sealed record InventoryReport(
        IReadOnlyList<Product> Products,
        ulong TotalQuantity,
        decimal TotalValue,
        IReadOnlyList<CategoryInventorySummary> Categories)
    {
        public int ProductCount => Products.Count;
    }
}