namespace StockControl.Utils.Menu
{
    public static class DisplayMenu
    {
        public static void WriteOptions()
        {
            var backgroundColor = ConsoleColor.DarkCyan;
            var foregroundColor = ConsoleColor.White;
            MenuScreen.MenuColor(backgroundColor, foregroundColor);
            
            int lines = 11;
            int columns = 50;
            MenuScreen.MenuBorder(lines, columns);

            var leftMargin = 4;
            Console.SetCursorPosition(leftMargin, 3);
            Console.Write("Stock Control System");
            Console.SetCursorPosition(leftMargin, 4);
            Console.WriteLine("------------------------");
            Console.SetCursorPosition(leftMargin, 5);
            Console.WriteLine("1. Add Product (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 6);
            Console.WriteLine("2. Remove Product (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 7);
            Console.WriteLine("3. View Products (Not Implemented)");
            Console.SetCursorPosition(leftMargin, 9);
            Console.WriteLine("0. Exit");
            Console.SetCursorPosition(leftMargin, 10);
            Console.WriteLine("------------------------");
            Console.SetCursorPosition(leftMargin, 11);
            Console.Write("option: ");
            Console.SetCursorPosition(0, 14);
        }
    }
}