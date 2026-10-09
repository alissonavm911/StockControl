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
        public static bool IsInitialized { get; }

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
                    new InitializeSalesException(ex.Message));
            }
        }

        public static List<Sale> GetSales()
        {
            return Sales;
        }
    }
}