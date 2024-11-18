using Microsoft.AspNetCore.Mvc;
using notify.Dtos;
using notify.Interfaces;
using notify.Models;

namespace notify.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PreferencesController : ControllerBase
{
    private readonly IPreferenceService _preferenceService;

    public PreferencesController(IPreferenceService preferenceService)
    {
        _preferenceService = preferenceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePreference([FromBody] PreferenceCreateDto preference)
    {
        var createdPreference = await _preferenceService.CreatePreferenceAsync(preference);
        return Ok(new { preference = createdPreference });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPreference(Guid id)
    {
        var preference = await _preferenceService.GetPreferenceAsync(id);
        return Ok(preference);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePreference(Guid id, [FromBody] Preference preference)
    {
        var updatedPreference = await _preferenceService.UpdatePreferenceAsync(id, preference);
        return Ok(updatedPreference);
    }
}