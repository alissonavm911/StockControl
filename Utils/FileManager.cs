using StockControl.Exceptions;
using StockControl.Handlers;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        private static readonly JsonSerializerOptions JsonOptions = CreateJsonOptions();

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

                var jsonString = JsonSerializer.Serialize(content, JsonOptions);
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

                return JsonSerializer.Deserialize<List<T>>(content, JsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new FileOperationException("Error occurred while reading from file: " + ex.Message), false);
                return null;
            }
        }

        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new DateOnlyJsonConverter());
            return options;
        }

        private sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
        {
            public override DateOnly Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.String)
                {
                    throw new JsonException("Expected a date string.");
                }

                var value = reader.GetString();
                if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    return date;
                }

                if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateTime))
                {
                    return DateOnly.FromDateTime(dateTime);
                }

                throw new JsonException($"Invalid date value: {value}");
            }

            public override void Write(
                Utf8JsonWriter writer,
                DateOnly value,
                JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            }
        }
    }
}