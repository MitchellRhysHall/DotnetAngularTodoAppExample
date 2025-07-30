using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.Database;

namespace TodoApp.Controllers;

[ApiController]
[Route("/api/user")]
public sealed class UserController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public UserController(ILogger<TodoController> logger, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!result.Succeeded) return Unauthorized();

        return Ok(new { success = true });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists != null)
        {
            return BadRequest(new { message = "Username already taken." });
        }

        var user = new User {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { message = "Registration failed", errors });
        }

        return Ok(new { message = "Registration successful" });
    }
    
    [HttpGet("status")]
    public IActionResult Status()
    {
        var isAuthenticated = User.Identity?.IsAuthenticated == true;
        return Ok(isAuthenticated);
    }
}