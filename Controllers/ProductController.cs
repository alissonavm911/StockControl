using StockControl.Exceptions;
using StockControl.Handlers;
using StockControl.Utils;
using StockControl.Models;
using StockControl.Services;

namespace StockControl.Controllers
{
    public static class ProductController
    {
        public static void AddProduct()
        {
            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
            Console.Clear();
            Background.MenuBorder(18, 50);
            var leftMargin = 3;
            
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("Add Product");
            Console.SetCursorPosition(leftMargin, 4);
            Console.WriteLine("-----------");
            Console.SetCursorPosition(leftMargin, 5);
            Console.Write("Enter product name: ");
            string? name = Console.ReadLine();
            Console.SetCursorPosition(leftMargin, 6);
            Console.Write("Enter product price: ");
            Console.SetCursorPosition(leftMargin, 7);
            decimal? price = decimal.TryParse(Console.ReadLine(), out decimal parsedPrice) ? parsedPrice : null;
            Console.Write("Enter product quantity: ");
            Console.SetCursorPosition(leftMargin, 8);
            uint? quantity = uint.TryParse(Console.ReadLine(), out uint parsedQuantity) ? parsedQuantity : null;

            Console.Clear();
            Background.MenuBorder(18, 50);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Select product category:");
            for (int i = 0; i < Enum.GetValues(typeof(ProductCategory)).Length; i++)
            {
                Console.SetCursorPosition(leftMargin, 3 + i);
                Console.WriteLine($"{i + 1}. {Enum.GetName(typeof(ProductCategory), i)}");
            }
            Console.SetCursorPosition(leftMargin, 3 + Enum.GetValues(typeof(ProductCategory)).Length);
            Console.Write("Enter category number: ");
            int? categoryIndex = int.TryParse(Console.ReadLine(), out int parsedCategoryIndex) ? parsedCategoryIndex : null;
            
            var productDto = new ProductDto()
            {
                Name = name?.Trim(),
                Price = price,
                Quantity = quantity,
                Category = (ProductCategory?)categoryIndex
            };

            try
            {
                ProductService.AddProduct(productDto);
            }
            catch
            {
                HandlerException.HandleException(new ProductInformationInvalidException(
                    "Product information is invalid. Please check the details and try again."));
            }

            Console.Clear();
            Background.MenuBorder(5, 50);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Product added successfully!"); 
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("Returning to menu....");
            Thread.Sleep(2000);
            MenuController.Menu();
            
        }
    }
}