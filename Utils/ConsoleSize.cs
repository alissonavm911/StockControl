namespace StockControl.Utils
{
    public static class ConsoleSize
    {
        public const int MinimumWidth = 103;
        public const int MinimumHeight = 22;

        public static bool EnsureMinimumSize()
        {
            while (true)
            {
                if (Console.WindowWidth >= MinimumWidth && Console.WindowHeight >= MinimumHeight)
                {
                    return true;
                }

                string? resizeError = null;
                if (OperatingSystem.IsWindows())
                {
                    try
                    {
                        var bufferWidth = Math.Max(Console.BufferWidth, MinimumWidth);
                        var bufferHeight = Math.Max(Console.BufferHeight, MinimumHeight);
                        if (bufferWidth != Console.BufferWidth || bufferHeight != Console.BufferHeight)
                        {
                            Console.SetBufferSize(bufferWidth, bufferHeight);
                        }

                        var windowWidth = Math.Max(Console.WindowWidth, MinimumWidth);
                        var windowHeight = Math.Max(Console.WindowHeight, MinimumHeight);
                        if (windowWidth != Console.WindowWidth || windowHeight != Console.WindowHeight)
                        {
                            Console.SetWindowSize(windowWidth, windowHeight);
                        }
                    }
                    catch (IOException ex)
                    {
                        resizeError = ex.Message;
                    }
                    catch (PlatformNotSupportedException ex)
                    {
                        resizeError = ex.Message;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        resizeError = ex.Message;
                    }
                    catch (InvalidOperationException ex)
                    {
                        resizeError = ex.Message;
                    }
                }
                else
                {
                    resizeError = "O ajuste automatico so e suportado no Windows.";
                }

                if (Console.WindowWidth >= MinimumWidth && Console.WindowHeight >= MinimumHeight)
                {
                    return true;
                }

                Console.WriteLine(
                    $"O terminal precisa ter pelo menos {MinimumWidth} colunas por {MinimumHeight} linhas.");
                if (resizeError != null)
                {
                    Console.WriteLine($"Este terminal nao permite ajuste automatico: {resizeError}");
                }

                Console.WriteLine("Aumente o terminal e pressione uma tecla para tentar novamente, ou Q para sair.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                    return false;
            }
        }
    }
}
