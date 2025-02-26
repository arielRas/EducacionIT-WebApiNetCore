using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class UserLogin
{
    [JsonPropertyOrder(0)]
    public required string Username {get; set;}

    [JsonPropertyOrder(1)]
    public required string Password {get; set;}
}
