using StockControl.Models;
using StockControl.Handlers;
using StockControl.Exceptions;
using StockControl.Utils;
using StockControl.Data;

namespace StockControl.Services
{
    public class SaleService
    {
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
    }
}