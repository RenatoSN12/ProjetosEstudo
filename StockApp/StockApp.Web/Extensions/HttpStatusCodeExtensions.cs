using System.Net;

namespace StockApp.Web.Extensions;

public static class HttpStatusCodeExtensions
{
    public static string StringParse(this HttpStatusCode httpStatusCode) => ((int)httpStatusCode).ToString();
}