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
        public static bool IsInitialized { get; }

        static Stock()
        {
            try
            {
                Products = new List<Product>();
                var loadedProducts = FileManager.ReadFromFile<Product>(DefaultFileName);
                if (loadedProducts == null)
                {
                    return;
                }

                if (loadedProducts.Count > 0)
                {
                    Products.AddRange(loadedProducts);
                }

                IsInitialized = true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new InitializeStockException(ex.Message), false);
            }
        }

        public static bool AddProduct(Product product)
        {
            if (!EnsureInitialized())
            {
                return false;
            }

            try
            {
                Products.Add(product);
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while adding the product: " + ex.Message));
                return false;
            }
        }

        public static bool RemoveProduct(Product product)
        {
            if (!EnsureInitialized())
            {
                return false;
            }

            try
            {
                var productIndex = Products.FindIndex(p => p.Id == product.Id);
                if (productIndex < 0)
                {
                    HandlerException.HandleException(new ProductNotFoundException("Product not found in the stock."));
                    return false;
                }

                Products.RemoveAt(productIndex);
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while removing the product: " + ex.Message));
                return false;
            }
        }

        public static List<Product> GetProducts()
        {
            return Products;
        }

        public static IReadOnlyList<Product>? GetProductsSnapshot()
        {
            if (!EnsureInitialized())
            {
                return null;
            }

            try
            {
                return Products.ToArray();
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while preparing a stock snapshot: " + ex.Message));
                return null;
            }
        }

        public static Product GetProductById(Guid id)
        {
            if (!EnsureInitialized())
            {
                return default;
            }

            try
            {
                var product = Products.FirstOrDefault(p => p.Id == id);
                if (product.Id == Guid.Empty)
                {
                    HandlerException.HandleException(new ProductNotFoundException("Product not found in the stock."));
                    return default;
                }

                return product;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while getting the product: " + ex.Message));
                return default;
            }
        }

        public static bool UpdateProduct(Product updatedProduct)
        {
            if (!EnsureInitialized())
            {
                return false;
            }

            try
            {
                var productIndex = Products.FindIndex(p => p.Id == updatedProduct.Id);
                if (productIndex < 0)
                {
                    HandlerException.HandleException(new ProductNotFoundException("Product not found in the stock."));
                    return false;
                }

                Products[productIndex] = updatedProduct;
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while updating the product: " + ex.Message));
                return false;
            }
        }

        private static bool EnsureInitialized()
        {
            if (IsInitialized)
            {
                return true;
            }

            HandlerException.HandleException(new InitializeStockException(
                "Inventory is unavailable because it could not be loaded. Correct the data file and restart the application."));
            return false;
        }
    }
}