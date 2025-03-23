namespace Stock.Domain.Requests;

public abstract class Request()
{
    protected string UserId { get; set; } = string.Empty;
}