using StockControl.Exceptions;
using StockControl.Handlers;
using StockControl.Models;
using StockControl.Services;
using StockControl.Utils;

namespace StockControl.Controllers
{
    public class SaleController
    {
        public static void AddSale()
        {
            var (year, month, day) = AddSaleDate();
            var products = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
            var saleDate = new DateOnly(year, month, day);

            while (true)
            {
                Console.Clear();
                Background.MenuBorder(10, 60);
                const int leftMargin = 3;
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Sale Editor");
                Console.SetCursorPosition(leftMargin, 3);
                Console.WriteLine("-------------------------");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine($"Sale Date: {saleDate:yyyy-MM-dd}");
                Console.SetCursorPosition(leftMargin, 5);
                Console.WriteLine($"Products added: {products.Count}");
                Console.SetCursorPosition(leftMargin, 7);
                Console.WriteLine("1 - Add Product");
                Console.SetCursorPosition(leftMargin, 8);
                Console.WriteLine("2 - Save Sale");
                Console.SetCursorPosition(leftMargin, 10);
                Console.Write("Option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        var (name, quantity) = GetProduct();
                        products[name] = products.TryGetValue(name, out var currentQuantity)
                            ? checked(currentQuantity + quantity)
                            : quantity;
                        break;
                    case "2":
                        if (products.Count == 0)
                        {
                            Console.SetCursorPosition(leftMargin, 9);
                            Console.WriteLine("Add at least one product before saving.");
                            Console.ReadKey();
                            break;
                        }

                        SaleService.AddSale(new SaleDto
                        {
                            Date = saleDate,
                            Products = products
                        });
                        return;
                    default:
                        Console.SetCursorPosition(leftMargin, 9);
                        Console.WriteLine("Invalid option. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static (int year, int month, int day) AddSaleDate()
        {
            Console.Clear();
            Background.MenuBorder(10, 60);
            const int leftMargin = 3;
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Sale Date");

            int year = ReadInteger("Year: ", leftMargin, 4, 1, 9999);
            int month = ReadInteger("Month: ", leftMargin, 5, 1, 12);
            int day = ReadInteger("Day: ", leftMargin, 6, 1, DateTime.DaysInMonth(year, month));

            return (year, month, day);
        }

        public static (string nameProduct, uint quantityProduct) GetProduct()
        {
            const int leftMargin = 3;
            string nameProduct;
            do
            {
                Console.Clear();
                Background.MenuBorder(7, 60);
                Console.SetCursorPosition(leftMargin, 2);
                Console.Write("Product Name: ");
                nameProduct = Console.ReadLine()?.Trim() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(nameProduct));

            uint quantityProduct;
            while (true)
            {
                Console.SetCursorPosition(leftMargin, 3);
                Console.Write("Product Quantity: ");
                if (uint.TryParse(Console.ReadLine(), out quantityProduct) && quantityProduct > 0)
                {
                    return (nameProduct, quantityProduct);
                }

                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine("Enter a quantity greater than zero.");
            }
        }

        public static void RemoveSale()
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
            Console.WriteLine("Remove Sale");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("-----------");
            Console.SetCursorPosition(leftMargin, 4);
            Console.Write("Enter sale ID: ");

            if (!Guid.TryParse(Console.ReadLine(), out var saleId))
            {
                HandlerException.HandleException(
                    new SaleInformationInvalidException("Sale ID is invalid."));
                return;
            }

            try
            {
                if (!SaleService.RemoveSale(saleId))
                {
                    return;
                }
            }
            catch (SaleNotFoundException ex)
            {
                HandlerException.HandleException(ex);
                return;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException(
                    "Error occurred while removing the sale: " + ex.Message));
                return;
            }

            Console.Clear();
            Background.MenuBorder(5, 50);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Sale removed successfully!");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true);
        }

        public static void ViewSales()
        {
            try
            {
                if (!ConsoleSize.EnsureMinimumSize())
                {
                    return;
                }

                var leftMargin = 3;
                var sales = SaleService.GetSales();
                if (sales == null)
                {
                    return;
                }

                if (sales.Count == 0)
                {
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                    Background.MenuBorder(8, GetBorderWidth());
                    Console.SetCursorPosition(leftMargin, 4);
                    Console.WriteLine("No sales available.");
                    Console.SetCursorPosition(leftMargin, 6);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    return;
                }

                var page = 0;
                var totalPages = sales.Count;

                while (true)
                {
                    if (!ConsoleSize.EnsureMinimumSize())
                    {
                        return;
                    }

                    var sale = sales[page];
                    var items = sale.Items ?? new List<SaleItem>();
                    var maxItemLines = Math.Max(1, Console.WindowHeight - 14);
                    var visibleItems = items.Take(maxItemLines).ToList();
                    var itemLines = Math.Max(1, visibleItems.Count);
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                    Background.MenuBorder(10 + itemLines, GetBorderWidth());
                    Console.SetCursorPosition(leftMargin, 2);
                    Console.WriteLine("View Sales");
                    Console.SetCursorPosition(leftMargin, 3);
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.SetCursorPosition(leftMargin, 4);
                    Console.WriteLine($"Sale Id: {sale.Id}");
                    Console.SetCursorPosition(leftMargin, 5);
                    Console.WriteLine($"Date: {sale.Date:yyyy-MM-dd}");
                    Console.SetCursorPosition(leftMargin, 6);
                    Console.WriteLine($"Total: {sale.TotalValue:C}");
                    Console.SetCursorPosition(leftMargin, 7);
                    Console.WriteLine("Items:");

                    if (items.Count == 0)
                    {
                        Console.SetCursorPosition(leftMargin, 8);
                        Console.WriteLine("No items in this sale.");
                    }
                    else
                    {
                        for (int i = 0; i < visibleItems.Count; i++)
                        {
                            var item = visibleItems[i];
                            Console.SetCursorPosition(leftMargin, 8 + i);
                            Console.WriteLine(
                                $"Name: {item.Product.Name} | Quantity: {item.Quantity} | Value: {item.Value:C}");
                        }
                    }

                    Console.SetCursorPosition(leftMargin, 8 + itemLines);
                    Console.WriteLine("---------------------");
                    Console.SetCursorPosition(leftMargin, 9 + itemLines);
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

        public static void ExportFinancialReport()
        {
            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            try
            {
                var report = SaleService.GetFinancialReport();
                if (report == null)
                {
                    return;
                }

                const int leftMargin = 3;
                Console.Clear();
                Background.MenuColor(ConsoleColor.DarkCyan, ConsoleColor.White);
                Background.MenuBorder(11, 90);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Financial Report");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine($"Sales: {report.SaleCount}");
                Console.SetCursorPosition(leftMargin, 5);
                Console.WriteLine($"Total revenue: {report.TotalRevenue:C}");
                Console.SetCursorPosition(leftMargin, 6);
                Console.WriteLine($"Products to restock: {report.RestockProductNames.Count}");
                Console.SetCursorPosition(leftMargin, 8);
                Console.WriteLine("The export includes restock alerts, top products and all sales.");
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
                Console.WriteLine("Export Financial Report");
                Console.SetCursorPosition(leftMargin, 4);
                Console.WriteLine("Enter the destination file path (the extension is optional):");
                Console.SetCursorPosition(leftMargin, 6);
                Console.Write("Path: ");
                var destinationPath = Console.ReadLine();

                var format = (InventoryReportFormat)formatSelection;
                var exportedPath = FinancialReportExporter.Export(report, destinationPath, format);
                if (exportedPath == null)
                {
                    return;
                }

                Console.Clear();
                Background.MenuBorder(6, 90);
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Financial report exported successfully.");
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
                    "Error occurred while generating the financial report: " + ex.Message));
            }
        }

        private static int GetBorderWidth()
        {
            return Math.Max(1, Math.Min(100, Console.WindowWidth - 2));
        }

        private static int ReadInteger(string label, int leftMargin, int top, int minimum, int maximum)
        {
            while (true)
            {
                Console.SetCursorPosition(leftMargin, top);
                Console.Write(label);
                if (int.TryParse(Console.ReadLine(), out var value) && value >= minimum && value <= maximum)
                {
                    return value;
                }

                Console.SetCursorPosition(leftMargin, top + 1);
                Console.Write($"Enter a value from {minimum} to {maximum}.          ");
            }
        }
    }
}
