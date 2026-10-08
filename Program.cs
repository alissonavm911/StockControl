using StockControl.Controllers;
using StockControl.Exceptions;
using StockControl.Handlers;
using StockControl.Utils;
using StockControl.Data;

namespace StockControl
{
    class Program
    {
        static void Main()
        {
            try
            {
                MenuController.Menu();
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new InitializeSystemException(ex.Message));
            }
            finally
            {
                Console.Clear();
                Background.MenuBorder(18,50);
                var leftMargin = 3;
                Console.SetCursorPosition(leftMargin,2);
                Console.WriteLine("Saving data in secure form.... Don't the Terminal!");
                Thread.Sleep(500);
                FileManager.SaveToFile("Inventory", Stock.GetProducts().ToArray());
                Thread.Sleep(1000);
                Console.SetCursorPosition(leftMargin,3);
                Console.WriteLine("Data saved successfully!");
                Thread.Sleep(2000);
            }
        }
    }
}