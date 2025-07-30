namespace TodoApp.Models.Requests;

public sealed record TodoCreateRequest
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}