using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class EditionResponseDto : EditionDto
{
    [JsonPropertyOrder(5)]
    public string? Isbn { get; set; } 

    [JsonPropertyOrder(6)]
    [Required] 
    public required string Book { get; set; } 

    [JsonPropertyOrder(7)]
    [Required]    
    public required string Editorial { get; set; }  

    [JsonPropertyOrder(8)]
    [Required]    
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}