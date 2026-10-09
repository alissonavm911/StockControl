using StockControl.Exceptions;
using StockControl.Models;
using StockControl.Handlers;
using StockControl.Utils;

namespace StockControl.Data
{
    public static class SalesRepository
    {
        private static List<Sale> Sales { get; } = new();
        private const string DefaultFileName = "sales";
        private static bool IsInitialized { get; }

        static SalesRepository()
        {
            try
            {
                Sales = new List<Sale>();
                var loadedSales = FileManager.ReadFromFile<Sale>(DefaultFileName);
                if (loadedSales == null)
                {
                    return;
                }

                if (loadedSales.Count > 0)
                {
                    Sales.AddRange(loadedSales);
                }

                IsInitialized = true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new InitializeSalesException(ex.Message), false);
            }
        }
        public static List<Sale> GetSales()
        {
            return Sales;
        }
        public static bool AddSale(Sale sale)
        {
            if (!EnsureInitialized())
            {
                return false;
            }

            try
            {
                Sales.Add(sale);
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Error ocurred while adding the sale: " + ex.Message));
                return false;
            }
        }
        public static bool RemoveSale(Sale sale)
        {
            if (!EnsureInitialized())
            {
                return false;
            }

            try
            {
                var saleIndex = Sales.FindIndex(s => s.Id == sale.Id);
                if (saleIndex < 0)
                {
                    HandlerException.HandleException(
                        new SaleNotFoundException("Sale not found in the repository."));
                    return false;
                }

                Sales.RemoveAt(saleIndex);
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while removing the sale: " + ex.Message));
                return false;
            }
        }
        public static bool UpdateSale(Sale updatedSale)
        {
            if (!EnsureInitialized())
            {
                return false;
            }
            
            try
            {
                var saleIndex = Sales.FindIndex(p => p.Id == updatedSale.Id);
                if (saleIndex < 0)
                {
                    HandlerException.HandleException(new ProductNotFoundException("Product not found in the stock."));
                    return false;
                }

                Sales[saleIndex] = updatedSale;
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while updating the sale: " + ex.Message));
                return false;
            }
        }
        private static bool EnsureInitialized()
        {
            if (IsInitialized)
            {
                return true;
            }

            HandlerException.HandleException(
                new InitializeSalesException(
                    "Sales Repository is unavailable because it could not be loaded. Correct the data file and restart the application."));
            return false;
        }
    }
}