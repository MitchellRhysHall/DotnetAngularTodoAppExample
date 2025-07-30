namespace TodoApp.Models.Responses;

public sealed record TodoReadResponse
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required bool IsDone { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
}
