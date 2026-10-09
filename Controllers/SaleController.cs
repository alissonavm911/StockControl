using StockControl.Handlers;
using StockControl.Utils;
using StockControl.Models;
using StockControl.Services;

namespace StockControl.Controllers
{
    public class SaleController
    {
        public static void AddSale()
        {
            Console.Clear();
            Background.MenuBorder(10, 50);
            var leftMargin = 3;
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Sale Editor");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("---------------------------------");
            Console.SetCursorPosition(leftMargin, 4);
            Console.WriteLine("Sale Date");
            Console.SetCursorPosition(leftMargin, 5);
            Console.Write("Year: ");
            int year = Int32.TryParse(Console.ReadLine(), out year) ? year : DateTime.Now.Year;
            Console.SetCursorPosition(leftMargin, 6);
            Console.Write("Month: ");
            int month = Int32.TryParse(Console.ReadLine(), out month) ? month : DateTime.Now.Month;
            Console.SetCursorPosition(leftMargin, 7);
            Console.Write("Day: ");
            int day = Int32.TryParse(Console.ReadLine(), out day) ? day : DateTime.Now.Day;
            Console.Clear();
            Background.MenuBorder(20, 50);
            Console.SetCursorPosition(leftMargin, 2);
            Console.WriteLine("Sale Editor");
            Console.SetCursorPosition(leftMargin, 3);
            Console.WriteLine("---------------------------------");
            Console.SetCursorPosition(leftMargin, 4);
            Console.WriteLine("1 - Add Product");
            Console.SetCursorPosition(leftMargin, 5);
            Console.WriteLine("2 - Save Sale");
            int option;
            option = Int32.TryParse(Console.ReadLine(), out option) ? option : 0;
            string nameProduct = string.Empty;
            uint quantityProduct = 0;
            switch (option)
            {
                case 1:
                    (nameProduct, quantityProduct)  = GetProduct();
                    if (nameProduct == string.Empty)
                    {
                        HandlerException.HandleException(
                            new ArgumentNullException("Product name can't null"));
                    }
                    break;
                case 2:
                    break;
                default:
                    HandlerException.HandleException(
                        new ArgumentNullException("The option can't null"));
                    break;
            }
            SaleDto sale = new SaleDto
            {
                Date = new DateTime(year, month, day),
                Products = new Dictionary<string, uint>
                {
                    {nameProduct, quantityProduct}
                }
            };
            SaleService.AddSale(sale);
        }
        public static (string nameProduct, uint quantityProduct) GetProduct()
        {
            Console.Clear();
            Background.MenuBorder(5,50);
            var leftMargin = 3;
            Console.SetCursorPosition(leftMargin, 2);
            Console.Write("Product Name: ");
            var nameProduct = Console.ReadLine() ?? string.Empty;
            Console.SetCursorPosition(leftMargin, 3);
            Console.Write("Product Quantity: ");
            uint quantityProduct = UInt32.TryParse(Console.ReadLine(), out quantityProduct) ? quantityProduct : 0;
            if (quantityProduct < 1)
            {
                HandlerException.HandleException(
                    new ArgumentNullException("It has to be at least one unit of the product."));
            }
            return (nameProduct, quantityProduct);
        }
    }
}