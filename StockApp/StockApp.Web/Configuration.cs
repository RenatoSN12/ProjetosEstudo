using MudBlazor;

namespace StockApp.Web;

public static class Configuration
{
    public const string HttpClientName = "StockApp";
    public static string BackendUrl { get; set; } = string.Empty;

    public static MudTheme Theme = new()
    {
        PaletteLight = new PaletteLight
        {
            Background = Colors.Gray.Lighten4,
        }
    };
}