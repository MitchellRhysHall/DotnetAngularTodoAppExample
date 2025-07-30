using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models.Database;

[Table("todos")]
[PrimaryKey(nameof(UserId), nameof(Title))]
public sealed record Todo
{
    [MaxLength(100)]
    public required string UserId { get; set; }
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(1000)]
    public required string Description { get; set; }
    public required bool IsDone { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public required DateTimeOffset UpdatedAt { get; set; }
}