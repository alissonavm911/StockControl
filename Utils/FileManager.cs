using StockControl.Exceptions;
using StockControl.Handlers;
using System.Text.Json;

namespace StockControl.Utils
{
    public static class FileManager
    {
        // The folder where is data saved
        private static readonly string DefaultFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StockControl"
        );

        // The extension in data saved file
        private static readonly string DefaultExtension = ".json";

        public static bool SaveToFile<TProduct>(string fileName, List<TProduct>? content)
        {
            string? temporaryPath = null;
            try
            {
                if (!fileName.EndsWith(DefaultExtension, StringComparison.OrdinalIgnoreCase))
                {
                    fileName += DefaultExtension;
                }

                var fullPath = Path.Combine(DefaultFolder, fileName);
                if (!PathValidator.ValidatePath(fullPath))
                {
                    return false;
                }

                var jsonString = JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true });
                temporaryPath = $"{fullPath}.{Guid.NewGuid():N}.tmp";
                File.WriteAllText(temporaryPath, jsonString);
                File.Move(temporaryPath, fullPath, true);
                return true;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new FileOperationException("Error occurred while saving to file: " + ex.Message));
                return false;
            }
            finally
            {
                if (temporaryPath != null && File.Exists(temporaryPath))
                {
                    try
                    {
                        File.Delete(temporaryPath);
                    }
                    catch (Exception ex)
                    {
                        HandlerException.HandleException(
                            new FileOperationException("Error occurred while removing temporary file: " + ex.Message));
                    }
                }
            }
        }

        public static List<T>? ReadFromFile<T>(string fileName)
        {
            try
            {
                if (!fileName.EndsWith(DefaultExtension, StringComparison.OrdinalIgnoreCase))
                {
                    fileName += DefaultExtension;
                }

                var fullPath = Path.Combine(DefaultFolder, fileName);
                if (!PathValidator.ValidatePath(fullPath, false))
                {
                    return null;
                }

                // Se o arquivo padrão ainda não existir (primeira execução, por exemplo), retorna um array vazio com segurança
                if (!File.Exists(fullPath))
                {
                    return new List<T>();
                }

                var content = File.ReadAllText(fullPath);

                return JsonSerializer.Deserialize<List<T>>(content) ?? new List<T>();
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new FileOperationException("Error occurred while reading from file: " + ex.Message), false);
                return null;
            }
        }
    }
}