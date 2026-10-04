namespace StockControl.Utils.Menu
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
                case 1: Console.WriteLine("Add Product selected (Not Implemented)"); break;
                case 2: Console.WriteLine("View Products selected (Not Implemented)"); break;
                case 3: Console.WriteLine("Update Product selected (Not Implemented)"); break;
                case 0: Console.WriteLine("Exit (Not Implemented)"); break;
            }
        }
    }
}