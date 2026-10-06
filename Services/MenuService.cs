using StockControl.Utils;
using StockControl.Controllers;
using StockControl.Exceptions;

namespace StockControl.Services
{
    class MenuService
    {
        public static void HandleMenu(string? option)
        {
            uint optionSelected = uint.TryParse(option, out uint optionInt) ? optionInt : 10;

            switch (optionSelected)
            {
                case 1: ProductController.AddProduct(); break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: break;
                case 7: break;
                case 8: break;
                case 9: break;
                case 0: Exit.ExitProgram(); break; 
                default: throw new OptionNotFoundException("Option not found. Please enter a valid option.");
            }
        }
    }
}