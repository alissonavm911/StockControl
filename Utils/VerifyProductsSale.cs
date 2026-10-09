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
                        var itemIndex = stock.FindIndex(p => p.Name == item.Key);
                        if (itemIndex == -1)
                        {
                            HandlerException.HandleException(
                                new ProductNotFoundException(item.Key + "Don't exist in stock"));
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