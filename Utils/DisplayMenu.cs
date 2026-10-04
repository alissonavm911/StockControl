namespace StockControl.Utils.Menu
{
    public static class DisplayMenu
    {
        public static void WriteOptions(Action<ConsoleColor, ConsoleColor> MenuColor, Action<int, int> MenuBorder)
        {
            var backgroundColor = ConsoleColor.DarkCyan;
            var foregroundColor = ConsoleColor.White;
            MenuColor(backgroundColor, foregroundColor);
        }
    }
}