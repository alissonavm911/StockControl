using StockControl.Exceptions;
using StockControl.Handlers;

namespace StockControl.Utils
{
    public class PathValidator
    {
        public static bool ValidatePath(string filePath, bool returnToMenu = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    HandlerException.HandleException(
                        new InvalidPathException("File path cannot be empty."), returnToMenu);
                    return false;
                }

                if (filePath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                {
                    HandlerException.HandleException(
                        new InvalidPathException("File path contains invalid characters."), returnToMenu);
                    return false;
                }

                var pathDirectory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(pathDirectory) && !Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }

                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(new InvalidPathException(ex.Message), returnToMenu);
                return false;
            }
        }
    }
}