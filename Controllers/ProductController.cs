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
            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
            Console.Clear();
            Background.MenuBorder(15, 100);
            var leftMargin = 3;
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Add Product");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("-----------");
            Console.SetCursorPosition(leftMargin, 4);
            Console.Write("Enter product name: ");
            string? name = Console.ReadLine();
            Console.SetCursorPosition(leftMargin, 5);
            Console.Write("Enter product price: ");
            decimal? price = decimal.TryParse(Console.ReadLine(), out decimal parsedPrice) ? parsedPrice : null;
            Console.SetCursorPosition(leftMargin, 6);
            Console.Write("Enter product quantity: ");
            uint? quantity = uint.TryParse(Console.ReadLine(), out uint parsedQuantity) ? parsedQuantity : null;
            Thread.Sleep(500);

            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            Console.Clear();
            Background.MenuBorder(16, 100);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Select product category:");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("------------------------");

            for (int i = 0; i < Enum.GetValues(typeof(ProductCategory)).Length; i++)
            {
                Console.SetCursorPosition(leftMargin, 4 + i);
                Console.WriteLine($"{i + 1}. {Enum.GetName(typeof(ProductCategory), i + 1)}");
            }

            Console.SetCursorPosition(leftMargin, 5 + Enum.GetValues(typeof(ProductCategory)).Length);
            Console.Write("Enter category number: ");
            int? categoryIndex = int.TryParse(Console.ReadLine(), out int parsedCategoryIndex)
                ? parsedCategoryIndex
                : null;

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

            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
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

        public static void ViewProducts()
        {
            try
            {
                if (!ConsoleSize.EnsureMinimumSize())
                {
                    return;
                }

                var leftMargin = 3;
                var products = ProductService.GetProducts();
                if (products.Count == 0)
                {
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                    Background.MenuBorder(8, GetBorderWidth());
                    Console.SetCursorPosition(leftMargin, 4);
                    Console.WriteLine("No products available.");
                    Console.SetCursorPosition(leftMargin, 6);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    MenuController.Menu();
                    return;
                }

                var pageSize = GetPageSize(3);
                var totalPages = (products.Count + pageSize - 1) / pageSize;
                var page = 0;

                while (true)
                {
                    if (!ConsoleSize.EnsureMinimumSize())
                    {
                        return;
                    }

                    var pageProducts = products.Skip(page * pageSize).Take(pageSize).ToList();
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                    Background.MenuBorder(8 + pageProducts.Count * 3, GetBorderWidth());
                    Console.SetCursorPosition(leftMargin, 2);
                    Console.WriteLine("View Products");
                    Console.SetCursorPosition(leftMargin, 3);
                    Console.WriteLine("-------------");

                    for (int i = 0; i < pageProducts.Count; i++)
                    {
                        var product = pageProducts[i];
                        Console.SetCursorPosition(leftMargin, 4 + i * 3);
                        Console.WriteLine($"Name: {product.Name}");
                        Console.SetCursorPosition(leftMargin, 5 + i * 3);
                        Console.WriteLine($"Price: {product.Price:C}");
                        Console.SetCursorPosition(leftMargin, 6 + i * 3);
                        Console.WriteLine("---------------------");
                    }

                    Console.SetCursorPosition(leftMargin, 7 + pageProducts.Count * 3);
                    Console.WriteLine($"Page {page + 1}/{totalPages} | N: next | P: previous | Q: return");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.N && page < totalPages - 1)
                    {
                        page++;
                    }
                    else if (key == ConsoleKey.P && page > 0)
                    {
                        page--;
                    }
                    else if (key != ConsoleKey.N && key != ConsoleKey.P)
                    {
                        MenuController.Menu();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(ex.Message));
            }
        }

        public static void RemoveProduct()
        {
            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
            Background.MenuBorder(8, 70);
            const int leftMargin = 3;
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Remove Product");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("--------------");
            Console.SetCursorPosition(leftMargin, 4);
            Console.Write("Enter product ID: ");

            if (!Guid.TryParse(Console.ReadLine(), out var productId))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product ID is invalid."));
                return;
            }

            try
            {
                ProductService.RemoveProduct(productId);
            }
            catch (ProductNotFoundException ex)
            {
                HandlerException.HandleException(ex);
                return;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while removing the product: " + ex.Message));
                return;
            }

            Console.Clear();
            Background.MenuBorder(5, 50);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Product removed successfully!");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true);
            MenuController.Menu();
        }

        public static void ViewInventory()
        {
            try
            {
                if (!ConsoleSize.EnsureMinimumSize())
                {
                    return;
                }

                var leftMargin = 3;
                var products = ProductService.GetProducts();
                if (products.Count == 0)
                {
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                    Background.MenuBorder(8, GetBorderWidth());
                    Console.SetCursorPosition(leftMargin, 4);
                    Console.WriteLine("No products available.");
                    Console.SetCursorPosition(leftMargin, 6);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    MenuController.Menu();
                    return;
                }

                var pageSize = GetPageSize(6);
                var totalPages = (products.Count + pageSize - 1) / pageSize;
                var page = 0;

                while (true)
                {
                    if (!ConsoleSize.EnsureMinimumSize())
                    {
                        return;
                    }

                    var pageProducts = products.Skip(page * pageSize).Take(pageSize).ToList();
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                    Background.MenuBorder(8 + pageProducts.Count * 6, GetBorderWidth());
                    Console.SetCursorPosition(leftMargin, 2);
                    Console.WriteLine("View Inventory");
                    Console.SetCursorPosition(leftMargin, 3);
                    Console.WriteLine("------------------------------------------------------------------");

                    for (int i = 0; i < pageProducts.Count; i++)
                    {
                        var product = pageProducts[i];
                        Console.SetCursorPosition(leftMargin, 4 + i * 6);
                        Console.WriteLine($"Product Id: {product.Id}");
                        Console.SetCursorPosition(leftMargin, 5 + i * 6);
                        Console.WriteLine($"Name: {product.Name}");
                        Console.SetCursorPosition(leftMargin, 6 + i * 6);
                        Console.WriteLine($"Quantity: {product.Quantity}");
                        Console.SetCursorPosition(leftMargin, 7 + i * 6);
                        Console.WriteLine($"Price: {product.Price:C}");
                        Console.SetCursorPosition(leftMargin, 8 + i * 6);
                        Console.WriteLine($"Category: {product.Category}");
                        Console.SetCursorPosition(leftMargin, 9 + i * 6);
                        Console.WriteLine("---------------------");
                    }

                    Console.SetCursorPosition(leftMargin, 9 + pageProducts.Count * 6);
                    Console.WriteLine($"Page {page + 1}/{totalPages} | N: next | P: previous | Q: return");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.N && page < totalPages - 1)
                    {
                        page++;
                    }
                    else if (key == ConsoleKey.P && page > 0)
                    {
                        page--;
                    }
                    else if (key != ConsoleKey.N && key != ConsoleKey.P)
                    {
                        MenuController.Menu();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(ex.Message));
            }
        }

        private static int GetPageSize(int linesPerProduct)
        {
            var availableLines = Math.Max(1, Console.WindowHeight - 11);
            return Math.Max(1, Math.Min(10, availableLines / linesPerProduct));
        }

        private static int GetBorderWidth()
        {
            return Math.Max(1, Math.Min(100, Console.WindowWidth - 2));
        }
    }
}