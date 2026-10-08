using StockControl.Exceptions;
using StockControl.Handlers;
using StockControl.Data;

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

            if (!Stock.IsInitialized)
            {
                Console.Clear();
                Background.MenuColor(ConsoleColor.DarkRed, ConsoleColor.White);
                Background.MenuBorder(5, 80);
                Console.SetCursorPosition(3, 2);
                Console.WriteLine("Inventory could not be loaded.");
                Console.SetCursorPosition(3, 3);
                Console.WriteLine("Existing data was not overwritten.");
                Console.SetCursorPosition(3, 4);
                Console.WriteLine("Press any key to exit without saving...");
                Console.ReadKey(true);
                Environment.Exit(1);
                return;
            }

            try
            {
                Console.Clear();
                Background.MenuBorder(5, 70);
                var leftMargin = 3;
                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("Saving data in secure form.... Don't the Terminal!");
                Thread.Sleep(3000);
                if (!FileManager.SaveToFile("inventory", Stock.GetProducts()))
                {
                    return;
                }

                Thread.Sleep(1000);
                Console.SetCursorPosition(leftMargin, 3);
                Console.WriteLine("Data saved successfully!");
                Thread.Sleep(2000);
            }
            catch (FileOperationException ex)
            {
                HandlerException.HandleException(ex);
                return;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Error in the data save operation: " + ex.Message));
                return;
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