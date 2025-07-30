using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Requests;

public sealed record TodoUpdateRequest
{
    [MaxLength(100)]
    public required string Title { get; init; }
    [MaxLength(1000)]
    public required string Description { get; init; }
    public required bool IsDone { get; init; }
}
