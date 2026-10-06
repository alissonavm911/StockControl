using StockControl.Controllers;
using StockControl.Utils;
using StockControl.Exceptions;

namespace StockControl.Handlers
{
    public static class HandlerException
    {
        public static void HandleException(Exception ex)
        {
            switch(ex)
            {
                case ProductInformationInvalidException:
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkRed, ConsoleColor.White);
                    Console.Clear();
                    Background.MenuBorder(5, 50);
                    Console.SetCursorPosition(4, 2);
                    Console.WriteLine($"Invalid product information: {ex.Message}");
                    Console.SetCursorPosition(4, 3);
                    Console.WriteLine("Returning to the menu...");
                    Thread.Sleep(3000);
                    MenuController.Menu();
                    break;
                default:
                    Console.Clear();
                    Background.MenuColor(ConsoleColor.DarkRed, ConsoleColor.White);
                    Console.Clear();
                    Background.MenuBorder(5, 50);
                    Console.SetCursorPosition(4, 2);
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.SetCursorPosition(4, 3);
                    Console.WriteLine("Returning to the menu...");
                    Thread.Sleep(3000);
                    MenuController.Menu();
                    break;
            }
            
            
        }
    }
}