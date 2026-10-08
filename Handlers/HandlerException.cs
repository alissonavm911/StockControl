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
                case ProductInformationInvalidException: ExceptionPrefixMessage("Invalid product information", ex); break;
                case ArgumentNullException: ExceptionPrefixMessage("Product null", ex); break;
                case ProductNotFoundException: ExceptionPrefixMessage("Product not found", ex); break;
                case ErrorOperationException: ExceptionPrefixMessage("Error in operation", ex); break;
                case ExitException: ExceptionPrefixMessage("Error while exiting the program", ex); break;
                case InitializeStockException: ExceptionPrefixMessage("Error while initializing stock", ex); break;
                case InitializeSystemException: ExceptionPrefixMessage("Error while initializing system", ex); break;
                case OptionNotFoundException: ExceptionPrefixMessage("Option not found", ex); break;
                case FileOperationException: ExceptionPrefixMessage("Error in file operation", ex); break;
                case InvalidPathException: ExceptionPrefixMessage("Invalid path", ex); break;
                default: ExceptionPrefixMessage("An error occurred", ex); break;
            }
        }
        
        public static void ExceptionPrefixMessage(string message , Exception ex)
        {
            Console.Clear();
            Background.MenuColor(ConsoleColor.DarkRed, ConsoleColor.White);
            Console.Clear();
            Background.MenuBorder(5, 100);
            Console.SetCursorPosition(4, 2);
            Console.WriteLine($"{message}: {ex.Message}");
            Console.SetCursorPosition(4, 3);
            Console.WriteLine("Returning to the menu...");
            Console.SetCursorPosition(4, 4);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            MenuController.Menu();
        }
    }
}