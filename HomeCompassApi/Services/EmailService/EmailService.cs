using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace HomeCompassApi.Services.EmailService
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailoptions)
        {
            _emailSettings = emailoptions.Value;
        }

        public async Task SendVerificationToken(string subject, string token, string toName, string toAddress)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = @"
                                    <h2>Your Confirmation Token Is:</h2>
                            <h3 style=""text-align: center;"">" + token + @"</h3>
                                     <h2>Don't share this token with anyone.</h2>";

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.GmailUsername, _emailSettings.GmailPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }


    }
}
