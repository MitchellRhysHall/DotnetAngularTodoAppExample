using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Requests;

public sealed record TodoCompleteRequest
{
    [MaxLength(100)]
    public required string Title { get; init; }
}
