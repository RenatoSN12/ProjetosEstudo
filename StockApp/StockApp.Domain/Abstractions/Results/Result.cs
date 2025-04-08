using System.Text.Json.Serialization;

namespace StockApp.Domain.Abstractions.Results;

[JsonDerivedType(typeof(Result<>), typeDiscriminator: "generic")]
public record Result
{
    [JsonConstructor]
    public Result()
    {
        Error = Error.None;
    }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; set; }
    
    public bool IsFailure => !IsSuccess;
    
    public Error Error { get; set; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);
    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);
}

public record Result<T> : Result
{
    public Result()
    {
    }
    
    public static Result<T> Failure(Error error) => new(default, false, error);
    public static Result<T> Success(T value) => new(value, true, Error.None);
 
    [JsonConstructor]
    public Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    [JsonPropertyName("value")]
    public T? Value { get; private set; }
}
