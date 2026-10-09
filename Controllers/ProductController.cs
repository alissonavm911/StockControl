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

            for (int i = 0; i < Enum.GetValues(typeof(EProductCategory)).Length; i++)
            {
                Console.SetCursorPosition(leftMargin, 4 + i);
                Console.WriteLine($"{i + 1}. {Enum.GetName(typeof(EProductCategory), i + 1)}");
            }

            Console.SetCursorPosition(leftMargin, 5 + Enum.GetValues(typeof(EProductCategory)).Length);
            Console.Write("Enter category number: ");
            int? categoryIndex = int.TryParse(Console.ReadLine(), out int parsedCategoryIndex)
                ? parsedCategoryIndex
                : null;

            var productDto = new ProductDto()
            {
                Name = name?.Trim(),
                Price = price,
                Quantity = quantity,
                Category = (EProductCategory?)categoryIndex
            };

            try
            {
                if (!ProductService.AddProduct(productDto))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while adding the product: " + ex.Message));
                return;
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
                if (products == null)
                {
                    return;
                }

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
                if (!ProductService.RemoveProduct(productId))
                {
                    return;
                }
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
        }

        public static void UpdateProduct()
        {
            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            const int leftMargin = 3;
            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
            Background.MenuBorder(8, 70);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Update Product");
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
                var product = ProductService.GetProduct(productId);
                if (product.Id == Guid.Empty)
                {
                    return;
                }

                if (!ConsoleSize.EnsureMinimumSize())
                {
                    return;
                }

                Console.Clear();
                Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                Background.MenuBorder(19, 100);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Update Product - leave a field blank to keep its current value");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine($"ID: {product.Id}");
                Console.SetCursorPosition(leftMargin, 5);
                Console.WriteLine($"Current name: {product.Name}");
                Console.SetCursorPosition(leftMargin, 6);
                Console.WriteLine($"Current price: {product.Price}");
                Console.SetCursorPosition(leftMargin, 7);
                Console.WriteLine($"Current quantity: {product.Quantity}");
                Console.SetCursorPosition(leftMargin, 8);
                Console.WriteLine($"Current category: {product.Category}");
                var categories = Enum.GetValues<EProductCategory>();
                for (var i = 0; i < categories.Length; i += 4)
                {
                    Console.SetCursorPosition(leftMargin, 9 + i / 4);
                    Console.WriteLine(string.Join(" | ", categories
                        .Skip(i)
                        .Take(4)
                        .Select(category => $"{(int)category}: {category}")));
                }

                Console.SetCursorPosition(leftMargin, 13);
                Console.Write("New name: ");
                var nameInput = Console.ReadLine();
                Console.SetCursorPosition(leftMargin, 14);
                Console.Write("New price: ");
                var priceInput = Console.ReadLine();
                Console.SetCursorPosition(leftMargin, 15);
                Console.Write("New quantity: ");
                var quantityInput = Console.ReadLine();
                Console.SetCursorPosition(leftMargin, 16);
                Console.Write("New category (number, blank to keep): ");
                var categoryInput = Console.ReadLine();

                var price = ParseOptionalDecimal(priceInput, "Product price", out var priceIsValid);
                var quantity = ParseOptionalQuantity(quantityInput, out var quantityIsValid);
                var category = ParseOptionalCategory(categoryInput, out var categoryIsValid);
                if (!priceIsValid || !quantityIsValid || !categoryIsValid)
                {
                    return;
                }

                var productDto = new ProductDto
                {
                    Name = string.IsNullOrWhiteSpace(nameInput) ? null : nameInput.Trim(),
                    Price = price,
                    Quantity = quantity,
                    Category = category
                };

                if (productDto.Name == null && productDto.Price == null &&
                    productDto.Quantity == null && productDto.Category == null)
                {
                    Console.Clear();
                    Background.MenuBorder(5, 60);
                    Console.SetCursorPosition(leftMargin, 2);
                    Console.WriteLine("No changes were requested.");
                    Console.SetCursorPosition(leftMargin, 3);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey(true);
                    return;
                }

                if (!ProductService.UpdateProduct(productId, productDto))
                {
                    return;
                }

                Console.Clear();
                Background.MenuBorder(5, 60);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Product updated successfully!");
                Console.SetCursorPosition(leftMargin, 3);
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey(true);
            }
            catch (ProductNotFoundException ex)
            {
                HandlerException.HandleException(ex);
            }
            catch (ProductInformationInvalidException ex)
            {
                HandlerException.HandleException(ex);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while updating the product: " + ex.Message));
            }
        }

        public static void ExportInventoryReport()
        {
            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            try
            {
                var report = ProductService.GetInventoryReport();
                if (report == null)
                {
                    return;
                }

                const int leftMargin = 3;
                Console.Clear();
                Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                Background.MenuBorder(10, 90);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Inventory Report");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine($"Product types: {report.ProductCount}");
                Console.SetCursorPosition(leftMargin, 5);
                Console.WriteLine($"Units in stock: {report.TotalQuantity}");
                Console.SetCursorPosition(leftMargin, 6);
                Console.WriteLine($"Total inventory value: {report.TotalValue:C}");
                Console.SetCursorPosition(leftMargin, 8);
                Console.WriteLine("The export includes totals by category and full product details.");
                Console.SetCursorPosition(leftMargin, 9);
                Console.WriteLine("1. TXT");
                Console.SetCursorPosition(leftMargin, 10);
                Console.Write("2. PDF | Select format: ");

                if (!int.TryParse(Console.ReadLine(), out var formatSelection) ||
                    !Enum.IsDefined(typeof(InventoryReportFormat), formatSelection))
                {
                    HandlerException.HandleException(
                        new OptionNotFoundException("Select 1 for TXT or 2 for PDF."));
                    return;
                }

                Console.Clear();
                Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                Background.MenuBorder(7, 90);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Export Inventory Report");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine("Enter the destination file path (the extension is optional):");
                Console.SetCursorPosition(leftMargin, 6);
                Console.Write("Path: ");
                var destinationPath = Console.ReadLine();

                var format = (InventoryReportFormat)formatSelection;
                var exportedPath = InventoryReportExporter.Export(report, destinationPath, format);
                if (exportedPath == null)
                {
                    return;
                }

                Console.Clear();
                Background.MenuBorder(6, 90);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Inventory report exported successfully.");
                Console.SetCursorPosition(leftMargin, 3);
                Console.WriteLine($"Format: {format}");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine($"File: {exportedPath}");
                Console.SetCursorPosition(leftMargin, 5);
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey(true);
            }
            catch (InvalidPathException ex)
            {
                HandlerException.HandleException(ex);
            }
            catch (FileOperationException ex)
            {
                HandlerException.HandleException(ex);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while generating the inventory report: " + ex.Message));
            }
        }

        private static decimal? ParseOptionalDecimal(string? input, string fieldName, out bool isValid)
        {
            isValid = true;
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (!decimal.TryParse(input, out var value))
            {
                HandlerException.HandleException(new ProductInformationInvalidException($"{fieldName} is invalid."));
                isValid = false;
                return null;
            }

            return value;
        }

        private static uint? ParseOptionalQuantity(string? input, out bool isValid)
        {
            isValid = true;
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (!uint.TryParse(input, out var value))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product quantity is invalid."));
                isValid = false;
                return null;
            }

            return value;
        }

        private static EProductCategory? ParseOptionalCategory(string? input, out bool isValid)
        {
            isValid = true;
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (!int.TryParse(input, out var value) ||
                !Enum.IsDefined(typeof(EProductCategory), value))
            {
                HandlerException.HandleException(
                    new ProductInformationInvalidException("Product category is invalid."));
                isValid = false;
                return null;
            }

            return (EProductCategory)value;
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
                if (products == null)
                {
                    return;
                }

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