using notify.Models;

namespace notify.Interfaces;

public interface IPreferenceRepository
{
    Task<Preference> CreatePreferenceAsync(Preference preference);
    Task<Preference> GetPreferenceAsync(Guid id);
    Task<Preference> UpdatePreferenceAsync(Guid id, Preference preference);
}