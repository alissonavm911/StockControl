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
    }
}