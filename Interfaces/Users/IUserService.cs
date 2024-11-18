using notify.Dtos;
using notify.Models;

namespace notify.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserCreateDto userCreateDto);
    Task<User?> GetUserAsync(Guid id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> UpdateUserAsync(Guid id, User user);
}