using System.Net;
using Microsoft.EntityFrameworkCore;
using notify.Data;
using notify.Exceptions;
using notify.Interfaces;
using notify.Models;

namespace notify.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApiContext _context;

    public UserRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var result = await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Preference)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await _context.Users
            .Include(u => u.Preference)
            .ToListAsync();

        return users;
    }

    public async Task<User> UpdateUserAsync(Guid id, User user)
    {
        var rowsAffected = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.Name, user.Name)
                .SetProperty(u => u.Phone, user.Phone)
                .SetProperty(u => u.PreferenceId, user.PreferenceId));

        if (rowsAffected == 0) throw new GlobalException("User not found", HttpStatusCode.NotFound);

        return (await GetUserAsync(id))!;
    }
}
