namespace BookStore.Services.DTOs;

public record class RoleDto
{
    public Guid Id {get; set;}
    public required string Name {get; set;}
}
