using StockControl.Utils;

namespace StockControl.Menu
{
    class MenuHandler
    {
        public static void HandleMenu(string? option)
        {
            if (!int.TryParse(option, out int optionInt))
            {
                Console.Clear();
                Background.MenuBorder(10, 50);
                Console.SetCursorPosition(4, 3);
                Console.WriteLine("Invalid option. Please enter a valid number.");
                Console.SetCursorPosition(0, 12);
            }

            switch (optionInt)
            {
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: break;
                case 7: break;
                case 8: break;
                case 9: break;
                case 0: Exit.ExitProgram(); break;
            }
        }
    }
}