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
        }
    }
}