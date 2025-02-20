using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record BookDto
{
    [Required]
    [JsonPropertyOrder(0)]
    public int Id { get; set; }

    [Required]
    [JsonPropertyOrder(1)]
    public required string Title { get; set; }

    [JsonPropertyOrder(3)]
    public string? Synopsis { get; set; }
}
