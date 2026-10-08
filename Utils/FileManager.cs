using StockControl.Handlers;
using StockControl.Exceptions;
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
        
        public static void SaveToFile<T>(string fileName, T[] content)
        {
            try
            {
                if (!fileName.EndsWith(DefaultExtension, StringComparison.OrdinalIgnoreCase))
                {
                    fileName += DefaultExtension;
                }
                
                var fullPath = Path.Combine(DefaultFolder, fileName);
                PathValidator.ValidatePath(fullPath);
                
                var jsonString = JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fullPath, jsonString);
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new FileOperationException("Error occurred while saving to file: " + ex.Message));
            }
        }

        public static T[] ReadFromFile<T>(string fileName)
        {
            try
            {
                if (!fileName.EndsWith(DefaultExtension, StringComparison.OrdinalIgnoreCase))
                {
                    fileName += DefaultExtension;
                }

                var fullPath = Path.Combine(DefaultFolder, fileName);

                // Se o arquivo padrão ainda não existir (primeira execução, por exemplo), retorna um array vazio com segurança
                if (!File.Exists(fullPath))
                {
                    return Array.Empty<T>();
                }

                var content = File.ReadAllText(fullPath);

                return JsonSerializer.Deserialize<T[]>(content) ?? Array.Empty<T>();
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new FileOperationException("Error occurred while reading from file: " + ex.Message));
                return new T[0];
            }
        }
    }
}