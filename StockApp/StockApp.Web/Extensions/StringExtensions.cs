namespace StockApp.Web.Extensions;

public static class StringExtensions
{
    public static string[] SplitErrors(this string errors) => errors.Split(['.'], StringSplitOptions.RemoveEmptyEntries);
}