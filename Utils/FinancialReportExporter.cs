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
    public static class FinancialReportExporter
    {
        private const double PageMargin = 42;
        private const double LineHeight = 14;
        private const uint RestockThreshold = 3;

        public static string? Export(
            FinancialReport report,
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
                        new OptionNotFoundException("Select a supported financial report format."));
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

        private static IReadOnlyList<ReportLine> BuildReportLines(FinancialReport report)
        {
            var culture = CultureInfo.GetCultureInfo("pt-BR");
            var lines = new List<ReportLine>
            {
                new("RELATÓRIO FINANCEIRO", IsHeading: true, IsTitle: true),
                new($"Gerado em: {DateTime.Now.ToString("dd/MM/yyyy HH:mm", culture)}"),
                new(""),
                new("RESUMO", true),
                new($"Quantidade de vendas: {report.SaleCount}"),
                new($"Receita total: {report.TotalRevenue.ToString("C", culture)}"),
                new($"Unidades vendidas: {report.UnitsSold}"),
                new($"Ticket médio: {report.AverageSaleValue.ToString("C", culture)}"),
                new($"Primeira venda: {FormatDate(report.FirstSaleDate, culture)}"),
                new($"Última venda: {FormatDate(report.LastSaleDate, culture)}"),
                new(""),
                new($"PRODUTOS PARA REABASTECER (menos de {RestockThreshold} unidades)", true)
            };

            if (report.RestockProductNames.Count == 0)
            {
                lines.Add(new ReportLine($"Nenhum produto abaixo de {RestockThreshold} unidades."));
            }
            else
            {
                lines.AddRange(report.RestockProductNames.Select(name => new ReportLine(name)));
            }

            lines.Add(new ReportLine(""));
            lines.Add(new ReportLine("PRODUTOS MAIS VENDIDOS", true));
            if (report.TopProducts.Count == 0)
            {
                lines.Add(new ReportLine("Nenhum item vendido."));
            }
            else
            {
                lines.AddRange(report.TopProducts.Select(product => new ReportLine(
                    $"{product.Name}: {product.QuantitySold} unidade(s), {product.TotalValue.ToString("C", culture)}")));
            }

            lines.Add(new ReportLine(""));
            lines.Add(new ReportLine("RECEITA POR CATEGORIA", true));
            if (report.RevenueByCategory.Count == 0)
            {
                lines.Add(new ReportLine("Nenhuma receita por categoria."));
            }
            else
            {
                lines.AddRange(report.RevenueByCategory.Select(category => new ReportLine(
                    $"{category.Name}: {category.QuantitySold} unidade(s), {category.TotalValue.ToString("C", culture)}")));
            }

            lines.Add(new ReportLine(""));
            lines.Add(new ReportLine("VENDAS", true));
            if (report.Sales.Count == 0)
            {
                lines.Add(new ReportLine("Nenhuma venda registrada."));
            }

            foreach (var sale in report.Sales)
            {
                lines.Add(new ReportLine($"ID: {sale.Id}"));
                lines.Add(new ReportLine($"Data: {sale.Date.ToString("dd/MM/yyyy", culture)}"));
                lines.Add(new ReportLine($"Total: {sale.TotalValue.ToString("C", culture)}"));
                lines.Add(new ReportLine("Itens:"));
                var items = sale.Items ?? new List<SaleItem>();
                if (items.Count == 0)
                {
                    lines.Add(new ReportLine("Nenhum item nesta venda."));
                }
                else
                {
                    lines.AddRange(items.Select(item => new ReportLine(
                        $"{item.Product.Name} | Quantidade: {item.Quantity} | Valor: {item.Value.ToString("C", culture)}")));
                }

                lines.Add(new ReportLine(""));
            }

            return lines;
        }

        private static string FormatDate(DateOnly? date, CultureInfo culture)
        {
            return date?.ToString("dd/MM/yyyy", culture) ?? "—";
        }

        private static void WritePdf(string path, FinancialReport report)
        {
            PdfFontSetup.EnsureWindowsFonts();

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
