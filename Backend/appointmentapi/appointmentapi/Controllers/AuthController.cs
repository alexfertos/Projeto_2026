using appointmentapi.DTOs.Auth;
using appointmentapi.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace appointmentapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _authService.RegisterUserAsync(dto);
            return Ok(new { Message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _authService.LoginUserAsync(dto);
        if (token is null)
            return Unauthorized(new { Error = "Invalid username or password" });

        return Ok(new { Token = token });
    }

    [HttpPut("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _authService.ResetPasswordAsync(dto);

            if (result is null)
                return NotFound(new { Error = "User not found" });

            return Ok(new { Message = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

}
