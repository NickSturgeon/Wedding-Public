namespace Wedding.Mail;

public class Settings
{
    public string[] To { get; init; } = Array.Empty<string>();
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;

    public string Server { get; init; } =  null!;
    public int Port { get; init; }
}
