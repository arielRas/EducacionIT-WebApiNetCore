using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs
{
    public record UserLoggedDto
    {
        [JsonPropertyOrder(0)]
        public required string Id { get; set; }

        [JsonPropertyOrder(1)]
        public required string Username { get; set; }

        [JsonPropertyOrder(2)]
        public required List<string> Roles { get; set; }
    }
}
