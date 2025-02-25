using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class EditionRequestCreateDto : EditionRequestUpdateDto
{   
    [JsonPropertyOrder(7)]
    public string? Isbn { get; set; }

    [Required]
    [JsonPropertyOrder(8)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }        
}