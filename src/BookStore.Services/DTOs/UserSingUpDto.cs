using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class UserSingUpDto
{
    [JsonPropertyOrder(0)]
    public required string Username {get; set;}

    [JsonPropertyOrder(1)]
    public required string Password {get; set;}

    [JsonPropertyOrder(0)]
    [EmailAddress]
    public required string Email { get; set; }
}
