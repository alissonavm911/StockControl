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
