using StockControl.Exceptions;
using StockControl.Utils;
using StockControl.Services;
using StockControl.Handlers;

namespace StockControl.Controllers
{
    public static class MenuController
    {
        public static void Menu()
        {
            while (true)
            {
                try
                {
                    if (!ConsoleSize.EnsureMinimumSize())
                    {
                        return;
                    }

                    var backgroundColor = ConsoleColor.DarkCyan;
                    var foregroundColor = ConsoleColor.White;
                    Background.MenuColor(backgroundColor, foregroundColor);

                    Background.MenuBorder(18, 100);

                    var leftMargin = 4;
                    Console.SetCursorPosition(leftMargin, 1);
                    Console.Write("Stock Control System");
                    Console.SetCursorPosition(leftMargin, 2);
                    Console.WriteLine("------------------------");
                    Console.SetCursorPosition(leftMargin, 3);
                    Console.WriteLine("1. Add Product ");
                    Console.SetCursorPosition(leftMargin, 4);
                    Console.WriteLine("2. Remove Product");
                    Console.SetCursorPosition(leftMargin, 5);
                    Console.WriteLine("3. Update Product");
                    Console.SetCursorPosition(leftMargin, 6);
                    Console.WriteLine("4. View Products ");
                    Console.SetCursorPosition(leftMargin, 7);
                    Console.WriteLine("5. Register Sale ");
                    Console.SetCursorPosition(leftMargin, 8);
                    Console.WriteLine("6. View Inventory ");
                    Console.SetCursorPosition(leftMargin, 9);
                    Console.WriteLine("7. Export Inventory Report");
                    Console.SetCursorPosition(leftMargin, 10);
                    Console.WriteLine("8. View Sales (Not Implemented)");
                    Console.SetCursorPosition(leftMargin, 11);
                    Console.WriteLine("9. Financial Report (Not Implemented)");
                    Console.SetCursorPosition(leftMargin, 13);
                    Console.WriteLine("0. Exit");
                    Console.SetCursorPosition(leftMargin, 15);
                    Console.WriteLine("------------------------");
                    Console.SetCursorPosition(leftMargin, 17);
                    Console.Write("option: ");
                    MenuService.HandleMenu(Console.ReadLine());
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    HandlerException.HandleException(
                        new ErrorOperationException("Error occurred while displaying the menu: " + ex.Message));
                }
            }
        }
    }
}