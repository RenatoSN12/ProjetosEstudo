using System.Text.Json.Serialization;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;

namespace StockApp.Domain.ValueObjects;

public sealed record Fullname
{
    public string FirstName { get;} = string.Empty;
    public string LastName { get; } = string.Empty;
    
    private Fullname(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    [JsonConstructor]
    private Fullname(){} // Apenas para deserialização
    public static Result<Fullname> Create(string firstName, string lastName)
    {
        if(firstName.Any(char.IsDigit) || lastName.Any(char.IsDigit))
            return Result.Failure<Fullname>(new Error("400", "Nomes não podem conter dígitos numéricos."));
        
        return Result.Success(new Fullname(firstName, lastName));
    }
    
    public override string ToString() => $"{FirstName} {LastName}";
}