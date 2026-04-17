using appointmentapi.Models.AuthEntity;
using appointmentapi.Repositories.Interface.AuthInterface;
using System.Security.Cryptography;
using System.Text;

namespace appointmentapi.Services.Auth;

public class UserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(Person person, string password)
    {
        string passwordHash; 
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            passwordHash = Convert.ToBase64String(hash);
        }

        var user = new User
        {
            PersonId = person.Id,
            Username = person.Email,
            PasswordHash = passwordHash,
            Active = true
        };

        await _userRepository.AddUserAsync(user);
        return user;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
       var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user is null)
            return null;

        string passwordHash;
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            passwordHash = Convert.ToBase64String(hash);
        }

        if (user.PasswordHash != passwordHash)
            return null;

        return user;
    }

    public async Task<User?> ResetAsync(string username, string newPassword)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user is null)
            return null;

        string newPasswordHash;

        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hash = sha256.ComputeHash(bytes);
            newPasswordHash = Convert.ToBase64String(hash);
        }

        await _userRepository.UpdatePasswordAsync(user, newPasswordHash);

        return user;
    }
}
