namespace StockApp.Domain;

public static class Configuration
{
    public static string ConnectionString { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
    public static int DefaultPageNumber { get; set; } = 0;
    public static int DefaultPageSize { get; set; } = 25;
} 