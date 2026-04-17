using appointmentapi.Models.AuthEntity;

namespace appointmentapi.Repositories.Interface.AuthInterface;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User?> GetUserByUsernameAsync(string username);
    Task UpdatePasswordAsync(User user, string newPasswordHash);
}
