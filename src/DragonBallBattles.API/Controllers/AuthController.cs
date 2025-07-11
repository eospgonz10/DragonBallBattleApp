using DragonBallBattles.Application.Services;
using DragonBallBattles.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonBallBattles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;
    
    public AuthController(AuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var token = _authService.Authenticate(request.Username, request.Password);
        if (token == null)
            return Unauthorized(new { message = "Credenciales inválidas" });
        
        var expiresInMinutes = int.TryParse(_configuration["Jwt:ExpiresInMinutes"], out var minutes) ? minutes : 60;
        
        return Ok(new LoginResponse 
        { 
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes)
        });
    }
}
