using StockControl.Utils;
using StockControl.Controllers;
using StockControl.Exceptions;
using StockControl.Handlers;

namespace StockControl.Services
{
    class MenuService
    {
        public static void HandleMenu(string? option)
        {
            try
            {
                uint optionSelected = uint.TryParse(option, out uint optionInt) ? optionInt : 10;

                switch (optionSelected)
                {
                    case 1:
                        Console.Clear();
                        ProductController.AddProduct();
                        break;
                    case 2:
                        Console.Clear();
                        ProductController.RemoveProduct();
                        break;
                    case 3:
                        Console.Clear();
                        ProductController.UpdateProduct();
                        break;
                    case 4:
                        Console.Clear();
                        ProductController.ViewProducts();
                        break;
                    case 5: Console.Clear(); break;
                    case 6:
                        Console.Clear();
                        ProductController.ViewInventory();
                        break;
                    case 7:
                        Console.Clear();
                        ProductController.ExportInventoryReport();
                        break;
                    case 8: Console.Clear(); break;
                    case 9: Console.Clear(); break;
                    case 0:
                        Console.Clear();
                        Exit.ExitProgram();
                        break;
                    default:
                        HandlerException.HandleException(
                            new OptionNotFoundException("Option not found. Please enter a valid option.")); break;
                }
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new ErrorOperationException("Error occurred while handling the menu option: " + ex.Message));
            }
        }
    }
}