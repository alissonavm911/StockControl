using System.Globalization;
using System.Text;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using StockControl.Exceptions;
using StockControl.Models;
using StockControl.Handlers;

namespace StockControl.Utils
{
    public enum InventoryReportFormat
    {
        Text = 1,
        Pdf = 2
    }

    public static class InventoryReportExporter
    {
        private const double PageMargin = 42;
        private const double LineHeight = 14;

        public static string? Export(
            InventoryReport report,
            string? destinationPath,
            InventoryReportFormat format)
        {
            string? temporaryPath = null;
            try
            {
                var fileExtension = format switch
                {
                    InventoryReportFormat.Text => ".txt",
                    InventoryReportFormat.Pdf => ".pdf",
                    _ => null
                };
                if (fileExtension == null)
                {
                    HandlerException.HandleException(
                        new OptionNotFoundException("Select a supported inventory report format."));
                    return null;
                }

                if (string.IsNullOrWhiteSpace(destinationPath))
                {
                    HandlerException.HandleException(
                        new InvalidPathException("A destination file path is required."));
                    return null;
                }

                var normalizedPath = destinationPath.Trim().Trim('"');
                var currentExtension = Path.GetExtension(normalizedPath);
                if (string.IsNullOrEmpty(currentExtension))
                {
                    normalizedPath += fileExtension;
                }
                else if (!string.Equals(currentExtension, fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    HandlerException.HandleException(new InvalidPathException(
                        $"The selected format requires a '{fileExtension}' file extension."));
                    return null;
                }

                var fullPath = Path.GetFullPath(normalizedPath);
                if (string.IsNullOrWhiteSpace(Path.GetFileNameWithoutExtension(fullPath)))
                {
                    HandlerException.HandleException(
                        new InvalidPathException("The destination path must include a file name."));
                    return null;
                }

                if (!PathValidator.ValidatePath(fullPath))
                {
                    return null;
                }

                temporaryPath = $"{fullPath}.{Guid.NewGuid():N}.tmp";
                if (format == InventoryReportFormat.Text)
                {
                    File.WriteAllLines(temporaryPath, BuildReportLines(report).Select(line => line.Text),
                        new UTF8Encoding(false));
                }
                else
                {
                    WritePdf(temporaryPath, report);
                }

                File.Move(temporaryPath, fullPath, true);
                return fullPath;
            }
            catch (Exception ex)
            {
                HandlerException.HandleException(
                    new FileOperationException("Error occurred while exporting the report: " + ex.Message));
                return null;
            }
            finally
            {
                try
                {
                    if (temporaryPath != null && File.Exists(temporaryPath))
                    {
                        File.Delete(temporaryPath);
                    }
                }
                catch (Exception ex)
                {
                    HandlerException.HandleException(
                        new FileOperationException("Error occurred while removing temporary report: " + ex.Message));
                }
            }
        }

        private static IReadOnlyList<ReportLine> BuildReportLines(InventoryReport report)
        {
            var culture = CultureInfo.GetCultureInfo("pt-BR");
            var lines = new List<ReportLine>
            {
                new("RELATÓRIO DE ESTOQUE", IsHeading: true, IsTitle: true),
                new($"Gerado em: {DateTime.Now.ToString("dd/MM/yyyy HH:mm", culture)}"),
                new(""),
                new("RESUMO", true),
                new($"Tipos de produto: {report.ProductCount}"),
                new($"Unidades em estoque: {report.TotalQuantity}"),
                new($"Valor total em estoque: {report.TotalValue.ToString("C", culture)}"),
                new(""),
                new("TOTAIS POR CATEGORIA", true)
            };

            lines.AddRange(report.Categories.Select(category => new ReportLine(
                $"{category.Category}: {category.ProductCount} produto(s), " +
                $"{category.TotalQuantity} unidade(s), {category.TotalValue.ToString("C", culture)}")));

            lines.Add(new ReportLine(""));
            lines.Add(new ReportLine("PRODUTOS", true));
            if (report.Products.Count == 0)
            {
                lines.Add(new ReportLine("Nenhum produto cadastrado."));
            }

            foreach (var product in report.Products)
            {
                lines.Add(new ReportLine($"ID: {product.Id}"));
                lines.Add(new ReportLine($"Nome: {product.Name}"));
                lines.Add(new ReportLine($"Categoria: {product.Category}"));
                lines.Add(new ReportLine($"Quantidade: {product.Quantity}"));
                lines.Add(new ReportLine($"Preço unitário: {product.Price.ToString("C", culture)}"));
                lines.Add(new ReportLine(
                    $"Valor em estoque: {(product.Price * product.Quantity).ToString("C", culture)}"));
                lines.Add(new ReportLine(""));
            }

            return lines;
        }

        private static void WritePdf(string path, InventoryReport report)
        {
            if (OperatingSystem.IsWindows())
            {
                GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            }

            using var document = new PdfDocument();
            var page = document.AddPage();
            page.Size = PageSize.A4;

            var regularFont = new XFont("Arial", 9);
            var headingFont = new XFont("Arial", 11, XFontStyleEx.Bold);
            var titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
            var graphics = XGraphics.FromPdfPage(page);
            var y = PageMargin;

            try
            {
                foreach (var line in BuildReportLines(report))
                {
                    var font = line.IsTitle ? titleFont : line.IsHeading ? headingFont : regularFont;
                    var brush = line.IsHeading ? XBrushes.DarkBlue : XBrushes.Black;
                    var maxWidth = page.Width.Point - PageMargin * 2;

                    foreach (var wrappedLine in WrapText(line.Text, graphics, font, maxWidth))
                    {
                        var lineHeight = Math.Max(LineHeight, font.Size + 4);
                        if (y + lineHeight > page.Height.Point - PageMargin)
                        {
                            graphics.Dispose();
                            page = document.AddPage();
                            page.Size = PageSize.A4;
                            graphics = XGraphics.FromPdfPage(page);
                            y = PageMargin;
                        }

                        graphics.DrawString(wrappedLine, font, brush, PageMargin, y);
                        y += lineHeight;
                    }
                }

                document.Save(path);
            }
            finally
            {
                graphics.Dispose();
            }
        }

        private static IEnumerable<string> WrapText(string text, XGraphics graphics, XFont font, double maxWidth)
        {
            if (string.IsNullOrEmpty(text))
            {
                yield return string.Empty;
                yield break;
            }

            var currentLine = new StringBuilder();
            foreach (var word in text.Split(' '))
            {
                var candidate = currentLine.Length == 0 ? word : $"{currentLine} {word}";
                if (graphics.MeasureString(candidate, font).Width <= maxWidth)
                {
                    currentLine.Clear();
                    currentLine.Append(candidate);
                    continue;
                }

                if (currentLine.Length > 0)
                {
                    yield return currentLine.ToString();
                    currentLine.Clear();
                }

                if (graphics.MeasureString(word, font).Width <= maxWidth)
                {
                    currentLine.Append(word);
                    continue;
                }

                foreach (var segment in WrapLongWord(word, graphics, font, maxWidth))
                {
                    yield return segment;
                }
            }

            if (currentLine.Length > 0)
            {
                yield return currentLine.ToString();
            }
        }

        private static IEnumerable<string> WrapLongWord(string word, XGraphics graphics, XFont font, double maxWidth)
        {
            var segment = new StringBuilder();
            foreach (var character in word)
            {
                var candidate = segment.ToString() + character;
                if (segment.Length > 0 && graphics.MeasureString(candidate, font).Width > maxWidth)
                {
                    yield return segment.ToString();
                    segment.Clear();
                }

                segment.Append(character);
            }

            if (segment.Length > 0)
            {
                yield return segment.ToString();
            }
        }

        private sealed record ReportLine(string Text, bool IsHeading = false, bool IsTitle = false);
    }
}