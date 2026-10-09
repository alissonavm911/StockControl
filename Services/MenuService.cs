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
                    case 1: ProductController.AddProduct(); break;
                    case 2: ProductController.RemoveProduct(); break;
                    case 3: ProductController.UpdateProduct(); break;
                    case 4: ProductController.ViewProducts(); break;
                    case 5: SaleController.AddSale(); break;
                    case 6: ProductController.ViewInventory(); break;
                    case 7: ProductController.ExportInventoryReport(); break;
                    case 8: break;
                    case 9: break;
                    case 0: Exit.ExitProgram(); break;
                    default: HandlerException.HandleException(new OptionNotFoundException("Option not found. Please enter a valid option.")); break;
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