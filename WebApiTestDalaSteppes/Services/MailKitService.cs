using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace WebApiTestDalaSteppes.Services
{
    public class MailKitService : IEmailSender
    {
        private readonly IConfiguration _config;
        public MailKitService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using var emailMessage = new MimeMessage();
            var fromAddress = _config["Smtp:FromAddress"];
            var password = _config["Smtp:Password"];
            var fromName = _config["Smtp:FromName"];

            emailMessage.From.Add(new MailboxAddress(fromName, fromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                var host = _config["Smtp:Host"];
                var port = int.Parse(_config["Smtp:Port"]);
                var useSsl = false;

                await client.ConnectAsync(host, port, useSsl);
                await client.AuthenticateAsync(fromAddress, password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
