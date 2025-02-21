using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class EditionResponseDto : EditionDto
{
    [JsonPropertyOrder(5)]
    public string? Isbn { get; set; } 

    
    [JsonPropertyOrder(6)]
    public string? Editorial { get; set; }     

    [Required]
    [JsonPropertyOrder(7)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}