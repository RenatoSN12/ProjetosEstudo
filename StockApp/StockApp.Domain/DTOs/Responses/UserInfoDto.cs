using System.Text.Json.Serialization;
using StockApp.Domain.ValueObjects;

namespace StockApp.Domain.DTOs.Responses;

public class UserInfoDto
{
    [JsonConstructor]
    private UserInfoDto(){}
    public UserInfoDto(string firstname, string lastname, string email)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
    }

    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public string FullName => $"{Firstname} {Lastname}";
}

