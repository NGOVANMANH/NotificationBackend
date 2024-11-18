using notify.Dtos;
using notify.Exceptions;
using notify.Interfaces;
using notify.Models;

namespace notify.Services;
public class EmailNotificationSubscriber : ISubscriber
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public EmailNotificationSubscriber(IEmailService emailService, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _emailService = emailService;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }
    public async Task UpdateAsync(NotificationDto notification, object? options = null)
    {
        await SendEmailNotificationAsync(notification);
    }

    private async Task SendEmailNotificationAsync(NotificationDto notification)
    {
        string userEmail = await GetUserEmail(notification.UserId);

        var senderEmail = _configuration.GetValue<string>("Mail:Email");
        if (string.IsNullOrEmpty(senderEmail))
            throw new InvalidOperationException("Sender email address is not configured.");

        var message = new MailMessageDto
        {
            From = senderEmail,
            To = new List<string> { userEmail },
            Subject = "Thông báo",
            Body = GenerateEmailBody(notification),
        };

        await _emailService.SendEmailAsync(message);
    }

    private string GenerateEmailBody(NotificationDto notification)
    {
        return $@"
            {notification.Message}, {notification.CreatedAt:yyyy-MM-dd}
        ";
    }

    private async Task<string> GetUserEmail(Guid userId)
    {
        var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        return (await userService.GetUserAsync(userId))!.Email;
    }
}
