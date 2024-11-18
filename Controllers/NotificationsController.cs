using Microsoft.AspNetCore.Mvc;
using notify.Dtos;
using notify.Interfaces;

namespace notify.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    [HttpPost]
    public async Task<IActionResult> NotifyToUsers([FromBody] NotificationReqDto notificationDto)
    {
        await _notificationService.CreateNotificationAsync(notificationDto);
        return NoContent();
    }
}