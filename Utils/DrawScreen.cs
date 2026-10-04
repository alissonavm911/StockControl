namespace StockControl.Utils
{
    public static class DrawScreen
    {
        public static void DrawColor(ConsoleColor color1, ConsoleColor color2)
        {
            Console.Clear();
            Console.BackgroundColor = color1;
            Console.ForegroundColor = color2;
            Console.Clear();
        }
        
        public static void DrawBorder(int lines, int columns)
        {
            Console.Write("+");
            for (int i = 0; i < columns; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            Console.WriteLine();

            for (int i = 0; i <= lines; i++)
            {
                Console.Write("|");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("|");
                Console.WriteLine();
            }

            Console.Write("+");
            for (int i = 0; i < columns; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
        }
    }
}