using notify.Dtos;

namespace notify.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(MailMessageDto email);
}