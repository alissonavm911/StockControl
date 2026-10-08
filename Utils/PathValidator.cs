using StockControl.Exceptions;

namespace StockControl.Utils
{
    public class PathValidator
    {
        public static bool ValidatePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new InvalidPathException("File path cannot be empty.");
            }

            if (filePath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new InvalidPathException("File path contains invalid characters.");
            }

            var pathDirectory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(pathDirectory) && !Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }

            return true;
        }
    }
}