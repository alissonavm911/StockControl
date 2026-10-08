using StockControl.Exceptions;
using StockControl.Handlers;
using ArgumentException = System.ArgumentException;
using FormatException = System.FormatException;

namespace StockControl.Utils
{
    public class PathValidator
    {
        public static bool ValidatePath(string filePath)
        {
            try
            {
                // if the path is null
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    HandlerException.HandleException(
                        new ArgumentException("Error occurred while reading file: " + filePath));
                }

                // if the path isn't formated
                if (filePath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                {
                    HandlerException.HandleException(
                        new FormatException("Error occurred while reading file: " + filePath));
                }

                var pathDirectory = Path.GetDirectoryName(filePath);
                // if the directory path, don't exist, create directory
                if (!string.IsNullOrEmpty(pathDirectory) && !Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new InvalidPathException(ex.Message));
                return false;
            }
        }
    }
}