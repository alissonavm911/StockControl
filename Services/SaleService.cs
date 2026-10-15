using StockControl.Models;
using StockControl.Handlers;
using StockControl.Exceptions;
using StockControl.Utils;
using StockControl.Data;

namespace StockControl.Services
{
    public class SaleService
    {
        private const uint RestockThreshold = 3;

        public static void AddSale(SaleDto dto)
        {
            try
            {
                var stock = Stock.GetProducts();
                var verified = VerifyProductsSale.VerifyProducts(stock, dto);
                if (!verified)
                {
                    return;
                }

                var finalSaleValue = 0m;
                List<SaleItem> saleItems = new();
                Dictionary<int, Product> updatedProducts = new();
                foreach (var item in dto.Products)
                {
                    string currentProductName = item.Key;
                    uint soldQuantity = item.Value;
                    var productIndex = stock.FindIndex(p => string.Equals(
                        p.Name,
                        currentProductName,
                        StringComparison.OrdinalIgnoreCase));
                    if (productIndex < 0)
                    {
                        throw new InvalidOperationException($"Product '{currentProductName}' does not exist in stock.");
                    }

                    var product = updatedProducts.TryGetValue(productIndex, out var updatedProduct)
                        ? updatedProduct
                        : stock[productIndex];
                    if (soldQuantity == 0 || soldQuantity > product.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Insufficient stock for '{currentProductName}'. Available: {product.Quantity}.");
                    }

                    product.Quantity -= soldQuantity;
                    var itemTotalValue = product.Price * soldQuantity;
                    finalSaleValue += itemTotalValue;
                    updatedProducts[productIndex] = product;
                    saleItems.Add(new SaleItem
                    {
                        Product = product,
                        Quantity = soldQuantity,
                        Value = itemTotalValue
                    });
                }

                Sale finalsale = new Sale
                {
                    Date = dto.Date,
                    Items = saleItems,
                    TotalValue = finalSaleValue
                };
                if (!SalesRepository.AddSale(finalsale))
                {
                    return;
                }

                foreach (var (index, product) in updatedProducts)
                {
                    stock[index] = product;
                }
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Failed to complete transaction: " + ex.Message));
            }
        }

        public static IReadOnlyList<Sale>? GetSales()
        {
            return SalesRepository.GetSalesSnapshot();
        }

        public static FinancialReport? GetFinancialReport()
        {
            try
            {
                var sales = SalesRepository.GetSalesSnapshot();
                if (sales == null)
                {
                    return null;
                }

                var products = Stock.GetProductsSnapshot();
                if (products == null)
                {
                    return null;
                }

                var orderedSales = sales
                    .OrderBy(sale => sale.Date)
                    .ThenBy(sale => sale.Id)
                    .ToArray();
                var soldItems = orderedSales
                    .SelectMany(sale => sale.Items ?? new List<SaleItem>())
                    .ToArray();
                var restockProductNames = products
                    .Where(product => product.Quantity < RestockThreshold && !string.IsNullOrWhiteSpace(product.Name))
                    .Select(product => product.Name!.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                    .ToArray();
                var topProducts = soldItems
                    .GroupBy(item => item.Product.Name?.Trim() ?? "Unknown", StringComparer.OrdinalIgnoreCase)
                    .Select(group => new ProductSalesSummary(
                        group.Key,
                        group.Aggregate(0UL, (total, item) => total + item.Quantity),
                        group.Sum(item => item.Value)))
                    .OrderByDescending(summary => summary.QuantitySold)
                    .ThenByDescending(summary => summary.TotalValue)
                    .Take(5)
                    .ToArray();
                var revenueByCategory = soldItems
                    .GroupBy(item => item.Product.Category)
                    .Select(group => new ProductSalesSummary(
                        group.Key.ToString(),
                        group.Aggregate(0UL, (total, item) => total + item.Quantity),
                        group.Sum(item => item.Value)))
                    .OrderByDescending(summary => summary.TotalValue)
                    .ToArray();

                return new FinancialReport(
                    orderedSales,
                    orderedSales.Sum(sale => sale.TotalValue),
                    soldItems.Aggregate(0UL, (total, item) => total + item.Quantity),
                    restockProductNames,
                    topProducts,
                    revenueByCategory);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while preparing the financial report: " + ex.Message));
                return null;
            }
        }

        public static bool RemoveSale(Guid id)
        {
            try
            {
                var sale = SalesRepository.GetSaleById(id);
                return sale.Id != Guid.Empty && SalesRepository.RemoveSale(sale);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while removing the sale: " + ex.Message));
                return false;
            }
        }
    }
}