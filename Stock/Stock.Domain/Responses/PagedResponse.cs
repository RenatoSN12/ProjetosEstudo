namespace Stock.Domain.Responses;

public record PagedResponse<T>(
    T? Data,
    int Code,
    int TotalCount,
    string Message = "",
    int PageSize = 50,
    int CurrentPage = 1)
    : Response<T>(Data, Code, Message)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}