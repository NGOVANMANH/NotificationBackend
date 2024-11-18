using notify.Models;

namespace notify.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserAsync(Guid id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> UpdateUserAsync(Guid id, User user);
}