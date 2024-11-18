namespace notify.Dtos;

public class MailMessageDto
{
    public string From { get; set; } = null!;
    public IEnumerable<string> To { get; set; } = new List<string>();
    public string Subject { get; set; } = null!;
    public IEnumerable<string> Cc { get; set; } = new List<string>();
    public string Body { get; set; } = null!;
}