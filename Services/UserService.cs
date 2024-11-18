using notify.Dtos;
using notify.Interfaces;
using notify.Models;

namespace notify.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = userCreateDto.Email,
            Name = userCreateDto.Name,
            Phone = userCreateDto.Phone,
            PreferenceId = userCreateDto.PreferenceId is null ? null : userCreateDto.PreferenceId,
        };

        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _userRepository.GetUserAsync(id);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task<User> UpdateUserAsync(Guid id, User user)
    {
        return await _userRepository.UpdateUserAsync(id, user);
    }
}