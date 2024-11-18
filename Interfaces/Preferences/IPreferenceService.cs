using notify.Dtos;
using notify.Models;

namespace notify.Interfaces;

public interface IPreferenceService
{
    Task<Preference> CreatePreferenceAsync(PreferenceCreateDto preference);
    Task<Preference> GetPreferenceAsync(Guid id);
    Task<Preference> UpdatePreferenceAsync(Guid id, Preference preference);
}