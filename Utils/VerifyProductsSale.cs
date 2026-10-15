using StockControl.Models;
using StockControl.Handlers;
using StockControl.Exceptions;

namespace StockControl.Utils
{
    public class VerifyProductsSale
    {
        public static bool VerifyProducts(List<Product> stock, SaleDto dto)
        {
            try
            {
                // Verify if all products exist
                
                    foreach (var item in dto.Products)
                    {
                        var matchingProducts = stock
                            .Where(product => string.Equals(
                                product.Name,
                                item.Key,
                                StringComparison.OrdinalIgnoreCase))
                            .Take(2)
                            .ToArray();
                        if (matchingProducts.Length == 0)
                        {
                            HandlerException.HandleException(
                                new ProductNotFoundException($"Product '{item.Key}' does not exist in stock."));
                            return false;
                        }

                        if (matchingProducts.Length > 1)
                        {
                            HandlerException.HandleException(
                                new ProductInformationInvalidException(
                                    $"More than one product named '{item.Key}' exists in stock. Rename duplicates before selling."));
                            return false;
                        }
                    }

                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException(ex.Message));
                return false;
            }
        }
    }
}