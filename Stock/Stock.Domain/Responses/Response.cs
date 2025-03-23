namespace Stock.Domain.Responses;

public record Response<T>(T? Data, int Code, string Message)
{
    public bool IsSuccess => Code is >= 200 and <= 299;
}