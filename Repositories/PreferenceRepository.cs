using System.Net;
using Microsoft.EntityFrameworkCore;
using notify.Data;
using notify.Exceptions;
using notify.Interfaces;
using notify.Models;

namespace notify.Repositories;
public class PreferenceRepository : IPreferenceRepository
{
    private readonly ApiContext _context;
    public PreferenceRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<Preference> CreatePreferenceAsync(Preference preference)
    {
        var result = await _context.Preferences.AddAsync(preference);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Preference> GetPreferenceAsync(Guid id)
    {
        var preference = await _context.Preferences
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (preference is null)
        {
            throw new GlobalException("Preference not found", HttpStatusCode.NotFound);
        }

        return preference;
    }

    public async Task<Preference> UpdatePreferenceAsync(Guid id, Preference preference)
    {
        var rowsAffected = await _context.Preferences
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(p => p.NotifyViaEmail, preference.NotifyViaEmail)
                .SetProperty(p => p.NotifyViaInApp, preference.NotifyViaInApp)
                .SetProperty(p => p.NotifyViaPush, preference.NotifyViaPush)
                .SetProperty(p => p.NotifyViaSMS, preference.NotifyViaSMS)
                .SetProperty(p => p.DoNotDisturb, preference.DoNotDisturb));

        if (rowsAffected == 0)
        {
            throw new GlobalException("Preference not found", HttpStatusCode.NotFound);
        }

        return await GetPreferenceAsync(id);
    }
}