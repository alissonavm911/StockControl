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
                    ExceptionPrefixMessage("Invalid product information", ex);
                    break;
                case ProductNotFoundByIdException:
                    ExceptionPrefixMessage("Product not found by ID", ex);
                    break;
                default:
                    ExceptionPrefixMessage("An error occurred", ex);
                    break;
            }
        }
        
        public static void ExceptionPrefixMessage(string message , Exception ex)
        {
            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkRed, ConsoleColor.White);
            Console.Clear();
            Background.MenuBorder(5, 50);
            Console.SetCursorPosition(4, 2);
            Console.WriteLine($"{message}: {ex.Message}");
            Console.SetCursorPosition(4, 3);
            Console.WriteLine("Returning to the menu...");
            Thread.Sleep(3000);
            MenuController.Menu();
        }
    }
}