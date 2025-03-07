namespace BookStore.Api.Models;

public record ApiError
{
    public DateTime TimeStamp {get;set;}
    public string? Path {get;set;}
    public int Status {get;set;}
    public string? Description {get;set;}
}
