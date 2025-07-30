using Mapster;
using TodoApp.Models.Responses;
using TodoApp.Repositories;

namespace TodoApp.Services;

public sealed class TodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly DatabaseRepository _repository;

    public TodoService(ILogger<TodoService> logger, DatabaseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
    public async Task<List<TodoReadResponse>> ReadAsync(string userId)
    {
        var todos = await _repository.ReadAsync(userId);

        return todos.Adapt<List<TodoReadResponse>>();
    }
    
    public async Task<TodoReadResponse?> ReadAsync(string userId, string title)
    {
        var todos = await _repository.ReadAsync(userId, title);
        return todos.Adapt<TodoReadResponse>();
    }

    public async Task CreateAsync(string userId, string title, string description)
    {
        await _repository.CreateTodoAsync(userId, title, description);
    }

    public async Task UpdateAsync(string userId, string title, string description, bool isDone)
    {
        await _repository.UpdateTodoAsync(userId, title, description, isDone);
    }

    public async Task DeleteAsync(string userId, string title)
    {
        await _repository.DeleteTodoAsync(userId, title);
    }

    public async Task CompleteAsync(string userId, string title)
    {
        await _repository.CompleteTodoAsync(userId, title);
    }
}