using appointmentapi.DTOs.Auth;
using appointmentapi.Models;
using appointmentapi.Repositories.Interface;
using appointmentapi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace appointmentapi.Services.Auth;

public class AuthService
{
    private readonly PersonService  _personService;
    private readonly UserService _userService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(PersonService personService, UserService userService, IOptions<JwtSettings> jwtOptions)
    {
        _personService = personService;
        _userService = userService;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task RegisterUserAsync(UserRegisterDTO dto)
    {
        var person = await _personService.CreatePersonAsync(
            dto.FullName, dto.Email, dto.PhoneNumber, dto.CPF, dto.Birthdate
            );

        await _userService.CreateUserAsync(person, dto.Password);

    }

    public async Task<string?> LoginUserAsync(UserLoginDTO dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            throw new ArgumentException("Username and password must be provided");

        var user = await _userService.LoginAsync(dto.Username, dto.Password);

        if (user is null)
            return null;

        var claims = new List<Claim>
        {
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
             new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
        signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string?> ResetPasswordAsync (ResetPasswordDTO dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.NewPassword))
            throw new ArgumentException("Username and password must be provided");

        var user = await _userService.ResetAsync(dto.Username, dto.NewPassword);

        if (user is null)
            return null; // usuário não encontrado

        return "Senha atualizada com sucesso!";


    }

}
