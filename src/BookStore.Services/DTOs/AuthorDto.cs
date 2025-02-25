using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record AuthorDto
{
    [Required]
    [JsonPropertyOrder(0)]
    public int Id { get; set; }

    [Required]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    [JsonPropertyOrder(2)]
    public string? LastName { get; set; }
}
