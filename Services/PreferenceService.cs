using notify.Dtos;
using notify.Exceptions;
using notify.Interfaces;
using notify.Models;

namespace notify.Services;

public class PreferenceService : IPreferenceService
{
    private readonly IPreferenceRepository _preferenceRepository;
    private readonly IUserService _userService;

    public PreferenceService(IPreferenceRepository preferenceRepository, IUserService userService)
    {
        _preferenceRepository = preferenceRepository;
        _userService = userService;
    }
    public async Task<Preference> CreatePreferenceAsync(PreferenceCreateDto preferenceCreateDto)
    {
        var preference = new Preference
        {
            Id = Guid.NewGuid(),
            UserId = preferenceCreateDto.UserId,
            NotifyViaEmail = preferenceCreateDto.NotifyViaEmail,
            NotifyViaSMS = preferenceCreateDto.NotifyViaSMS,
            NotifyViaPush = preferenceCreateDto.NotifyViaPush,
            NotifyViaInApp = preferenceCreateDto.NotifyViaInApp,
            DoNotDisturb = preferenceCreateDto.DoNotDisturb,
        };

        var newPreference = await _preferenceRepository.CreatePreferenceAsync(preference);

        var user = await _userService.GetUserAsync(preferenceCreateDto.UserId);

        if (user is null) throw new GlobalException("User is not found.", System.Net.HttpStatusCode.NotFound);

        await _userService.UpdateUserAsync(preferenceCreateDto.UserId, new User
        {
            Email = user.Email,
            Name = user.Name,
            Phone = user.Phone,
            PreferenceId = newPreference.Id
        });

        return newPreference;
    }

    public async Task<Preference> GetPreferenceAsync(Guid id)
    {
        return await _preferenceRepository.GetPreferenceAsync(id);
    }

    public async Task<Preference> UpdatePreferenceAsync(Guid id, Preference preference)
    {
        return await _preferenceRepository.UpdatePreferenceAsync(id, preference);
    }
}