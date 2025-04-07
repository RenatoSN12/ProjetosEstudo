using System.Text.Json.Serialization;

namespace StockApp.Domain.Abstractions.Results;

public record PagedResult<T> : Result<T>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    [JsonConstructor]
    public PagedResult(T? value, int pageNumber, int pageSize, int totalCount)
        : base(value, true, Error.None)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public static PagedResult<T> Failure(Error error)
    {
        return new PagedResult<T>(default, 0, 0, 0)
        {
            IsSuccess = false,
            Error = error
        };
    }
    public static PagedResult<T> Success(T value, int pageNumber, int pageSize, int totalCount)
        => new(value, pageNumber, pageSize, totalCount);
}