using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class UserLogin
{
    [JsonPropertyOrder(0)]
    [Required]
    public required string Username {get; set;}

    [JsonPropertyOrder(1)]
    [Required]
    [RegularExpression(@".*[A-Z].*", ErrorMessage = "The password must have at least one uppercase letter")]
    [Length(8, 20, ErrorMessage = "Password must be at least 8 characters long")]
    public required string Password {get; set;}
}