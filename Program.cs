using StockControl.Controllers;
using StockControl.Exceptions;
using StockControl.Handlers;

namespace StockControl
{
    class Program
    {
        static void Main()
        {
            try {
                MenuController.Menu();
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new InitializeSystemException(ex.Message));
            }
        }
    }
}