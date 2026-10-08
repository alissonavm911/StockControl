using StockControl.Handlers;
using StockControl.Exceptions;
using System.Text.Json;
using StockControl.Models;

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
        
        public static void SaveToFile<T>(string fileName, List<Product>? content)
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
                var temporaryPath = $"{fullPath}.{Guid.NewGuid():N}.tmp";
                File.WriteAllText(temporaryPath, jsonString);
                File.Move(temporaryPath, fullPath, true);
            }
            catch (Exception ex)
            {
                throw new FileOperationException("Error occurred while saving to file: " + ex.Message);
            }
        }

        public static List<T> ReadFromFile<T>(string fileName)
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
                    return new List<T>();
                }

                var content = File.ReadAllText(fullPath);

                return JsonSerializer.Deserialize<List<T>>(content) ?? new List<T>();
            }
            catch (Exception ex)
            {
                throw new FileOperationException("Error occurred while reading from file: " + ex.Message);
            }
        }
    }
}