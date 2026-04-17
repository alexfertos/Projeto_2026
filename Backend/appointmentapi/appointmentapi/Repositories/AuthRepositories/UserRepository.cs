using appointmentapi.Data;
using appointmentapi.Models.AuthEntity;
using appointmentapi.Repositories.Interface.AuthInterface;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Repositories.Auth;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .AsNoTracking().
            FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdatePasswordAsync(User user, string newPasswordHash)
    {
        user.PasswordHash = newPasswordHash;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
