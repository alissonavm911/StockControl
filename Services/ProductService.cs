using StockControl.Models;
using StockControl.Utils;
using StockControl.Data;
using StockControl.Exceptions;
using StockControl.Handlers;

namespace StockControl.Services
{
    public static class ProductService
    {
        public static void AddProduct(ProductDto productDto)
        {
            if (string.IsNullOrEmpty(productDto.Name) )
            {
                HandlerException.HandleException(new Exceptions.ProductInformationInvalidException("Product name must be provided."));
            }
            if (productDto.Price < 0)
            {
                HandlerException.HandleException(new Exceptions.ProductInformationInvalidException("Product price and quantity must be non-negative."));
            }
            if (productDto.Price == null)
            {
                HandlerException.HandleException(new Exceptions.ProductInformationInvalidException("Product price must be provided."));
            }
            if (productDto.Quantity == null)
            {
                HandlerException.HandleException(new Exceptions.ProductInformationInvalidException("Product quantity must be provided."));
            }
            if(productDto.Category == null || !Enum.IsDefined(typeof(ProductCategory), productDto.Category))
            {
                HandlerException.HandleException(new Exceptions.ProductInformationInvalidException("Product category is invalid."));
            }

            try
            {
                Product product = default;
                if (productDto.Name != null && productDto.Price != null && productDto.Quantity != null &&
                    productDto.Category != null)
                {
                    product = new Product()
                    {
                        Name = productDto.Name,
                        Price = productDto.Price.Value,
                        Quantity = productDto.Quantity.Value,
                        Category = productDto.Category.Value
                    };
                }
                Stock.AddProduct(product);
            } catch (Exception ex)
            {
                HandlerException.HandleException(new Exceptions.ErrorOperationException(ex.Message));
            }
        }
        public static List<Product> GetProducts()
        {
            var products = Stock.GetProducts();
            return products;
        }

        public static Product GetProduct(Guid id)
        {
            return Stock.GetProductById(id);
        }

        public static void RemoveProduct(Guid id)
        {
            var product = Stock.GetProducts().FirstOrDefault(p => p.Id == id);
            if (product.Id == Guid.Empty)
            {
                throw new ProductNotFoundException("Product not found in the stock.");
            }

            Stock.RemoveProduct(product);
        }

        public static void UpdateProduct(Guid id, ProductDto productDto)
        {
            if (productDto.Name != null && string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ProductInformationInvalidException("Product name cannot be empty.");
            }
            if (productDto.Price < 0)
            {
                throw new ProductInformationInvalidException("Product price must be non-negative.");
            }
            if (productDto.Category != null &&
                !Enum.IsDefined(typeof(ProductCategory), productDto.Category.Value))
            {
                throw new ProductInformationInvalidException("Product category is invalid.");
            }

            var currentProduct = Stock.GetProductById(id);
            var updatedProduct = currentProduct with
            {
                Name = productDto.Name?.Trim() ?? currentProduct.Name,
                Price = productDto.Price ?? currentProduct.Price,
                Quantity = productDto.Quantity ?? currentProduct.Quantity,
                Category = productDto.Category ?? currentProduct.Category
            };

            Stock.UpdateProduct(updatedProduct);
        }
    }
}