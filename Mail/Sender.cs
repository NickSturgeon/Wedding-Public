using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Wedding.Data.Models;

namespace Wedding.Mail;

public class Sender
{
    private Settings _settings;
    private ILogger<Sender> _log;

    public Sender(Settings settings, ILogger<Sender> log)
    {
        _settings = settings;
        _log = log;
    }

    public async Task SendMessage(Invitation invitation)
    {
        var action = invitation.SubmissionCount == 1 ? "Submitted" : "Updated";
        var subject = $"An RSVP was {action}";
        var body = GetBody(invitation);
        var message = CreateMessage(subject, body);

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_settings.Server, _settings.Port, SecureSocketOptions.StartTls);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            await client.SendAsync(message);
        }
        catch (Exception e)
        {
            // don't halt execution
            _log.LogCritical(e, "Code {Code}", invitation.Code);
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }

    private MimeMessage CreateMessage(string subject, string body)
    {
        var mime = new MimeMessage();

        mime.To.AddRange(_settings.To.Select(x => new MailboxAddress(x)));
        mime.From.Add(new MailboxAddress(_settings.UserName));
        mime.Subject = subject;
        mime.Body = new TextPart(MimeKit.Text.TextFormat.Text) 
        { 
            Text = body 
        };

        return mime;
    }

    private static string GetBody(Invitation invitation)
    {
        var separator = new string('-', 20);
        var body = new System.Text.StringBuilder();
        body.AppendLine($"Contact Email: {invitation.Email ?? "N/A"}");
        body.AppendLine($"Signed COVID-19 Vaccination Agreement: {invitation.CovidAgreement}");
        body.AppendLine($"Accommodations Needed: {invitation.AccomodationsNeeded}");
        body.AppendLine(separator);
        foreach (var attendee in invitation.Attendees)
        {
            body.AppendLine(attendee.Name);
            body.AppendLine($"Going: {attendee.RSVP}");
            if (attendee.RSVP.GetValueOrDefault())
            {
                body.AppendLine($"Vegetarian Option: {attendee.VegetarianOption}");
                if (!string.IsNullOrEmpty(attendee.DietaryRestrictions))
                {
                    body.AppendLine($"Dietary Requirements: {attendee.DietaryRestrictions}");
                }
            }
            body.AppendLine(separator);
        }
        body.AppendLine($"General Comments: {invitation.GeneralComments ?? "N/A"}");
        return body.ToString();
    }
}
