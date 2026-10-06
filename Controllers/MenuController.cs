using StockControl.Utils;
using StockControl.Services;

namespace StockControl.Controllers
{
    public static class MenuController
    {
        public static void Menu()
        {
            var backgroundColor = ConsoleColor.DarkCyan;
            var foregroundColor = ConsoleColor.White;
            Background.MenuColor(backgroundColor, foregroundColor);
            
            int lines = 18;
            int columns = 50;
            Background.MenuBorder(lines, columns);

            var leftMargin = 4;
            Console.SetCursorPosition(leftMargin, 3);
            Console.Write("Stock Control System");
            Console.SetCursorPosition(leftMargin, 4);
            Console.WriteLine("------------------------");
            Console.SetCursorPosition(leftMargin, 5);
            Console.WriteLine("1. Add Product (Implementing)");
            Console.SetCursorPosition(leftMargin, 6);
            Console.WriteLine("2. Remove Product (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 7);
            Console.WriteLine("3. Update Product (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 8);
            Console.WriteLine("4. View Products (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 9);
            Console.WriteLine("5. Register Sale (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 10);
            Console.WriteLine("6. View Sales (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 11);
            Console.WriteLine("7. Inventory Report (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 12);
            Console.WriteLine("8. View Inventory (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 13);
            Console.WriteLine("9. Financial Report (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 15);
            Console.WriteLine("0. Exit");
            Console.SetCursorPosition(leftMargin, 17);
            Console.WriteLine("------------------------");
            Console.SetCursorPosition(leftMargin, 18);
            Console.Write("option: ");
            MenuService.HandleMenu(Console.ReadLine());
        }
    }
}