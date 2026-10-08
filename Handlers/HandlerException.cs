using StockControl.Controllers;
using StockControl.Utils;
using StockControl.Exceptions;

namespace StockControl.Handlers
{
    public static class HandlerException
    {
        public static void HandleException(Exception ex, bool returnToMenu = true)
        {
            var message = ex switch
            {
                ProductInformationInvalidException => "Invalid product information",
                ArgumentNullException => "Product null",
                ProductNotFoundException => "Product not found",
                ErrorOperationException => "Error in operation",
                ExitException => "Error while exiting the program",
                InitializeStockException => "Error while initializing stock",
                InitializeSystemException => "Error while initializing system",
                OptionNotFoundException => "Option not found",
                FileOperationException => "Error in file operation",
                InvalidPathException => "Invalid path",
                _ => "An error occurred"
            };

            if (!returnToMenu)
            {
                Console.Error.WriteLine($"{message}: {ex.Message}");
                return;
            }

            ExceptionPrefixMessage(message, ex);
        }

        public static void ExceptionPrefixMessage(string message, Exception ex)
        {
            if (!ConsoleSize.EnsureMinimumSize())
            {
                return;
            }

            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkRed, ConsoleColor.White);
            Console.Clear();
            Background.MenuBorder(5, Math.Min(120, Console.WindowWidth - 3));
            Console.SetCursorPosition(3, 2);
            Console.WriteLine($"{message}: {ex.Message}");
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("Returning to the menu...");
            Console.SetCursorPosition(3, 4);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            MenuController.Menu();
        }
    }
}