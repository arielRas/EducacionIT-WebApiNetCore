using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class UserSignUp : UserLogin
{
    [JsonPropertyOrder(2)]
    [EmailAddress]
    public required string Email { get; set; }
}
