using StockControl.Exceptions;
using StockControl.Handlers;

namespace StockControl.Utils
{
    public static class Background
    {
        public static void MenuColor(ConsoleColor color1, ConsoleColor color2)
        {
            try
            {
                Console.Clear();
                Console.BackgroundColor = color1;
                Console.ForegroundColor = color2;
                Console.Clear();
            } catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException("Error occurred while setting menu colors: " + ex.Message));
            }
        }
        
        public static void MenuBorder(int lines, int columns)
        {
            try
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
            catch (Exception ex)
            {
                HandlerException.HandleException(new ErrorOperationException("Error occurred while drawing menu border: " + ex.Message));
            }
        }
    }
}