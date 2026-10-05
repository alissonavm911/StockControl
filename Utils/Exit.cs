namespace StockControl.Utils
{
    public static class Exit
    {
        public static void ExitProgram()
        {
            Console.Clear();
            Background.MenuBorder(2, 30);
            Console.SetCursorPosition(3, 2);
            Console.WriteLine("Exiting the program...");
            Thread.Sleep(2000);
            
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();    
            
            Environment.Exit(0);
        }
    }
}