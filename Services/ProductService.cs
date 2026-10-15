using StockControl.Models;
using StockControl.Utils;
using StockControl.Data;
using StockControl.Exceptions;
using StockControl.Handlers;

namespace StockControl.Services
{
    public static class ProductService
    {
        public static bool AddProduct(ProductDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product name must be provided."));
                return false;
            }

            if (productDto.Price < 0)
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product price and quantity must be non-negative."));
                return false;
            }

            if (productDto.Price == null)
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product price must be provided."));
                return false;
            }

            if (productDto.Quantity == null)
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product quantity must be provided."));
                return false;
            }

            if (productDto.Category == null || !Enum.IsDefined(typeof(EProductCategory), productDto.Category))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product category is invalid."));
                return false;
            }

            try
            {
                var normalizedName = productDto.Name.Trim();
                if (Stock.GetProducts().Any(product =>
                        string.Equals(product.Name?.Trim(), normalizedName, StringComparison.OrdinalIgnoreCase)))
                {
                    HandlerException.HandleException(
                        new ProductInformationInvalidException("A product with this name already exists."));
                    return false;
                }

                var product = new Product()
                {
                    Name = normalizedName,
                    Price = productDto.Price.Value,
                    Quantity = productDto.Quantity.Value,
                    Category = productDto.Category.Value
                };

                return Stock.AddProduct(product);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(ex.Message));
                return false;
            }
        }

        public static IReadOnlyList<Product>? GetProducts()
        {
            return Stock.GetProductsSnapshot();
        }

        public static InventoryReport? GetInventoryReport()
        {
            try
            {
                var products = Stock.GetProductsSnapshot();
                if (products == null)
                {
                    return null;
                }

                var categories = Enum.GetValues<EProductCategory>()
                    .Select(category =>
                    {
                        var categoryProducts = products.Where(product => product.Category == category).ToArray();
                        return new CategoryInventorySummary(
                            category,
                            categoryProducts.Length,
                            categoryProducts.Aggregate(0UL, (total, product) => total + product.Quantity),
                            categoryProducts.Sum(product => product.Price * product.Quantity));
                    })
                    .ToArray();

                return new InventoryReport(
                    products,
                    products.Aggregate(0UL, (total, product) => total + product.Quantity),
                    products.Sum(product => product.Price * product.Quantity),
                    categories);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while preparing the inventory report: " + ex.Message));
                return null;
            }
        }

        public static Product GetProduct(Guid id)
        {
            return Stock.GetProductById(id);
        }

        public static bool RemoveProduct(Guid id)
        {
            try
            {
                var product = Stock.GetProductById(id);
                return product.Id != Guid.Empty && Stock.RemoveProduct(product);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while removing the product: " + ex.Message));
                return false;
            }
        }

        public static bool UpdateProduct(Guid id, ProductDto productDto)
        {
            if (productDto.Name != null && string.IsNullOrWhiteSpace(productDto.Name))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product name cannot be empty."));
                return false;
            }

            if (productDto.Price < 0)
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product price must be non-negative."));
                return false;
            }

            if (productDto.Category != null &&
                !Enum.IsDefined(typeof(EProductCategory), productDto.Category.Value))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product category is invalid."));
                return false;
            }

            try
            {
                var currentProduct = Stock.GetProductById(id);
                if (currentProduct.Id == Guid.Empty)
                {
                    return false;
                }

                var normalizedName = productDto.Name?.Trim() ?? currentProduct.Name;
                if (Stock.GetProducts().Any(product =>
                        product.Id != id &&
                        string.Equals(product.Name?.Trim(), normalizedName, StringComparison.OrdinalIgnoreCase)))
                {
                    HandlerException.HandleException(
                        new ProductInformationInvalidException("A product with this name already exists."));
                    return false;
                }

                var updatedProduct = currentProduct with
                {
                    Name = normalizedName,
                    Price = productDto.Price ?? currentProduct.Price,
                    Quantity = productDto.Quantity ?? currentProduct.Quantity,
                    Category = productDto.Category ?? currentProduct.Category
                };

                return Stock.UpdateProduct(updatedProduct);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while updating the product: " + ex.Message));
                return false;
            }
        }
    }
}