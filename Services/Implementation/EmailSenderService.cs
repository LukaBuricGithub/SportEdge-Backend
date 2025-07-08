using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{


    /// <summary>
    /// Provides implementation for email sending-related service operations.
    /// </summary>
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration config;

        public EmailSenderService(IConfiguration config)
        {
            this.config = config;
        }

        /// <inheritdoc/>
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(config["EmailSettings:SmtpServer"], int.Parse(config["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(config["EmailSettings:Username"], config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        /// <inheritdoc/>
        public async Task SendReceiptEmailAsync(string to, string subject, string body, byte[]? attachment = null, string? attachmentFileName = null) 
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;


            var builder = new BodyBuilder { TextBody = body };

            if (attachment != null && !string.IsNullOrEmpty(attachmentFileName))
            {
                builder.Attachments.Add(attachmentFileName, attachment, ContentType.Parse("application/pdf"));
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(config["EmailSettings:SmtpServer"], int.Parse(config["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(config["EmailSettings:Username"], config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
