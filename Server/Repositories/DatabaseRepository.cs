using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoApp.Contexts;
using TodoApp.Models.Database;
using TodoApp.Settings;

namespace TodoApp.Repositories;

public sealed class DatabaseRepository
{
    private readonly ILogger<DatabaseRepository> _logger;
    private readonly IOptions<CookieSettings> _options;
    private readonly DatabaseContext _context;

    public DatabaseRepository(ILogger<DatabaseRepository> logger, IOptions<CookieSettings> options, DatabaseContext context)
    {
        _logger = logger;
        _options = options;
        _context = context;
    }
    
    public async Task<List<Todo>> ReadAsync(string userId)
    {
        return await _context.Todos.Where(x => x.UserId == userId).ToListAsync();
    }
    
    public async Task<Todo?> ReadAsync(string userId, string title)
    {
        return await _context.Todos.FindAsync(userId, title);
    }
    
    public async Task CreateTodoAsync(string userId, string title, string description)
    {
        var todo = new Todo
        {
            UserId = userId,
            Title = title,
            Description = description,
            IsDone = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        _context.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(string userId, string title, string description, bool isDone)
    {
        var todo = await _context.Todos.FindAsync(userId, title);
        if (todo == null)
        {
            return;
        }
        
        todo.Title = title;
        todo.Description = description;
        todo.IsDone = isDone;
        todo.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(string userId, string title)
    {
        var todo = await _context.Todos.FindAsync(userId, title);
        if (todo == null)
        {
            return;
        }

        _context.Remove(todo);
        await _context.SaveChangesAsync();
    }

    public async Task CompleteTodoAsync(string userId, string title)
    {
        var todo = await _context.Todos.FindAsync(userId, title);
        if (todo == null)
        {
            return;
        }
        todo.IsDone = true;
        todo.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync();
    }
}