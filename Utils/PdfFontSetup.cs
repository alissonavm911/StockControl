using PdfSharp.Fonts;

namespace StockControl.Utils
{
    internal static class PdfFontSetup
    {
        private static bool IsConfigured { get; set; }

        public static void EnsureWindowsFonts()
        {
            if (!OperatingSystem.IsWindows() || IsConfigured)
            {
                return;
            }

            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            IsConfigured = true;
        }
    }
}
