using System.Net;
using System.Net.Mail;
using notify.Dtos;
using notify.Interfaces;

namespace notify.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    public EmailService(IConfiguration configuration)
    {
        string host = configuration.GetSection("Mail:Host").Value!;
        int port = int.Parse(configuration.GetSection("Mail:Port").Value!);
        string email = configuration.GetSection("Mail:Email").Value!;
        string password = configuration.GetSection("Mail:Password").Value!;
        _smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(email, password),
            EnableSsl = true
        };
    }
    public async Task SendEmailAsync(MailMessageDto email)
    {
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(email.From),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true
        };

        foreach (var To in email.To)
        {
            mailMessage.To.Add(To);
        }

        foreach (var Cc in email.Cc)
        {
            mailMessage.CC.Add(Cc);
        }

        await _smtpClient.SendMailAsync(mailMessage);
    }
}