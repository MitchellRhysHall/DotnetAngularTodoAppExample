using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.Requests;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("/api/todo")]
[Authorize]
public sealed class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly TodoService _service;

    public TodoController(ILogger<TodoController> logger, TodoService service)
    {
        _logger = logger;
        _service = service;
    }
    
    [HttpPost]
    [Route("read-all")] 
    public async Task<ActionResult> ReadAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var todos = await _service.ReadAsync(userId);

        return Ok(todos);
    }
    
    [HttpPost]
    [Route("read")] 
    public async Task<ActionResult> ReadAsync(TodoReadRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var todo = await _service.ReadAsync(userId, request.Title);
        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }
    
    [HttpPost]
    [Route("create")] 
    public async Task<ActionResult> CreateAsync(TodoCreateRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var todo = await _service.ReadAsync(userId, request.Title);
        if (todo != null)
        {
            return Conflict();
        }
        
        await _service.CreateAsync(userId, request.Title, request.Description);
        
        return Ok();
    }
    
    [HttpPost]
    [Route("update")] 
    public async Task<ActionResult> UpdateAsync(TodoUpdateRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var todo = await _service.ReadAsync(userId, request.Title);
        if (todo == null)
        {
            return NotFound();
        }
        
        await _service.UpdateAsync(userId, request.Title, request.Description, request.IsDone);

        return Ok();
    }

    [HttpPost]
    [Route("delete")] 
    public async Task<ActionResult> DeleteAsync(TodoDeleteRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var todo = await _service.ReadAsync(userId, request.Title);
        if (todo == null)
        {
            return NotFound();
        }
        
        await _service.DeleteAsync(userId, request.Title);

        return Ok();
    }

    [HttpPost]
    [Route("complete")] 
    public async Task<ActionResult> CompleteAsync(TodoCompleteRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var todo = await _service.ReadAsync(userId, request.Title);
        if (todo == null)
        {
            return NotFound();
        }
        
        await _service.CompleteAsync(userId, request.Title);

        return Ok();
    }
}