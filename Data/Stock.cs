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
            try
            {
                if (Products.Count > 0)
                {
                    return Products.FirstOrDefault(p => p.Id == id);
                }
                HandlerException.HandleException(new ProductNotFoundException("Product not found in the stock"));
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Error occurred while getting the product: " + ex.Message));
            }
            return default;
        }

        public static void UpdateProduct(Product updatedProduct)
        {
            try
            {
                var updated = false;
                for (int i = 0; i < Products.Count; i++)
                {
                    if (Products[i].Id == updatedProduct.Id)
                    {
                        Products[i] = updatedProduct;
                        updated = true;
                        break;
                    }
                }
                if (!updated)
                {
                    HandlerException.HandleException(new ProductNotFoundException("Product not found in the stock"));
                }
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException("Error occurred while updating the product: " + ex.Message));
            }
        }
    }
}