using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record EditionDto
{
    [JsonPropertyOrder(0)]
    public Guid Id { get; set; } 

    [JsonPropertyOrder(1)]
    public DateOnly PublicationDate { get; set; }

    [JsonPropertyOrder(2)]
    public int Pages { get; set; }  

    [JsonPropertyOrder(3)]  
    public required string Language { get; set; }

    [JsonPropertyOrder(4)]
    public required string TypeCode { get; set; }
}