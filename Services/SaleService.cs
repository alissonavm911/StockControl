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
                foreach (var item in dto.Products)
                {
                    string currentProductName = item.Key;
                    uint soldQuantity = item.Value;
                    var productIndex = stock.FindIndex(p => p.Name == currentProductName);
                    var product = stock[productIndex];
                    product.Quantity -= soldQuantity;
                    var itemTotalValue = product.Price * soldQuantity;
                    finalSaleValue += itemTotalValue;
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
                SalesRepository.AddSale(finalsale);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Failed to complete transaction: " + ex.Message));
            }
    }
    }
}