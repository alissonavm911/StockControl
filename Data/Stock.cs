using StockControl.Models;
using StockControl.Exceptions;
using StockControl.Handlers;
using StockControl.Utils;

namespace StockControl.Data
{
    public static class Stock
    {
        private static List<Product> Products { get; } = new();
        private const string DefaultFileName = "inventory";
        public static bool IsInitialized { get; private set; }

        static Stock()
        {
            try
            {
                Products = new List<Product>();
                var loadedProducts = FileManager.ReadFromFile<Product>(DefaultFileName);

                if (loadedProducts.Count > 0)
                {
                    Products.AddRange(loadedProducts);
                }

                IsInitialized = true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new InitializeStockException(ex.Message));
            }
        }

        public static void AddProduct(Product product)
        {
            try
            {
                Products.Add(product);
            }
            catch (ArgumentNullException ex)
            {
                HandlerException.HandleException(new ProductInformationInvalidException(ex.Message));
            }
        }

        public static void RemoveProduct(Product product)
        {
            var productIndex = Products.FindIndex(p => p.Id == product.Id);
            if (productIndex < 0)
            {
                throw new ProductNotFoundException("Product not found in the stock.");
            }

            Products.RemoveAt(productIndex);
        }

        public static List<Product> GetProducts()
        {
            return Products;
        }

        public static Product GetProductById(Guid id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product.Id == Guid.Empty)
            {
                throw new ProductNotFoundException("Product not found in the stock.");
            }

            return product;
        }

        public static void UpdateProduct(Product updatedProduct)
        {
            var productIndex = Products.FindIndex(p => p.Id == updatedProduct.Id);
            if (productIndex < 0)
            {
                throw new ProductNotFoundException("Product not found in the stock.");
            }

            Products[productIndex] = updatedProduct;
        }
    }
}