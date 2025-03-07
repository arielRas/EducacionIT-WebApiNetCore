namespace BookStore.Api.Models;

public record ApiErrorCritical : ApiError
{
    public Guid Id {get;set;}
}
