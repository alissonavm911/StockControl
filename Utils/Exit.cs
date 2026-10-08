using StockControl.Exceptions;
using StockControl.Handlers;
using StockControl.Data;
using StockControl.Models;

namespace StockControl.Utils
{
    public static class Exit
    {
        public static void ExitProgram()
        {
            Console.Clear();
            Background.MenuBorder(2, 30);
            Console.SetCursorPosition(3, 2);
            Console.WriteLine("Exiting the program...");
            Thread.Sleep(2000);
            try
            {
                Console.Clear();
                Background.MenuBorder(5, 70);
                var leftMargin = 3;
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Saving data in secure form.... Don't the Terminal!");
                Thread.Sleep(3000);
                FileManager.SaveToFile<Product>("inventory", Stock.GetProducts());
                Thread.Sleep(1000);
                Console.SetCursorPosition(leftMargin, 3);
                Console.WriteLine("Data saved successfully!");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Error in the data saved file" + ex.Message));
            }
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new ExitException(ex.Message));
            }
        }
    }
}
