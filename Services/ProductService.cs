using StockControl.Models;
using StockControl.Utils;
using StockControl.Data;

namespace StockControl.Services
{
    public static class ProductService
    {
        public static bool AddProduct(ProductDto productDto)
        {
            if (string.IsNullOrEmpty(productDto.Name) )
            {
                throw new Exceptions.ProductInformationInvalidException("Product name must be provided.");
            }
            if (productDto.Price < 0)
            {
                throw new Exceptions.ProductInformationInvalidException("Product price and quantity must be non-negative.");
            }
            if (productDto.Price == null)
            {
                throw new Exceptions.ProductInformationInvalidException("Product price must be provided.");
            }
            if (productDto.Quantity == null)
            {
                throw new Exceptions.ProductInformationInvalidException("Product quantity must be provided.");
            }
            if(productDto.Category == null || !Enum.IsDefined(typeof(ProductCategory), productDto.Category))
            {
                throw new Exceptions.ProductInformationInvalidException("Product category is invalid.");
            }
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price.Value,
                Quantity = productDto.Quantity.Value,
                Category = productDto.Category.Value
            };
            Stock.AddProduct(product);
            return true;
        }
    }
}