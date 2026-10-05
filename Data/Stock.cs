using StockControl.Models;

namespace StockControl.Data
{
    public class Stock
    {
        private List<Product> Products { get; }

        public Stock()
        {
            Products = new List<Product>();
        }
    }
}