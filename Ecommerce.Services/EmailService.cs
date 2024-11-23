using Ecommerce.Core.IRepositories.IServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            // Create a new MimeMessage object to represent the email message.
            var emailMessage = new MimeMessage();
            // Add the sender's email address from configuration settings (from email).
            emailMessage.From.Add(new MailboxAddress("Knowledge Academy", configuration["EmailSettings:FromEmail"]));
            // Add the recipient's email address.
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            // Set the subject of the email.
            emailMessage.Subject = subject;
            // Set the body of the email message as plain text.
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message };

            // Create an SMTP client to send the email.
            var client = new SmtpClient();
            // Connect to the SMTP server using the server address, port, and SSL settings from configuration.
            await client.ConnectAsync(configuration["EmailSettings:SmtpServer"], int.Parse(configuration["EmailSettings:Port"])
                               , bool.Parse(configuration["EmailSettings:UseSSL"]));

            // Authenticate with the SMTP server using the sender's email and password.
            await client.AuthenticateAsync(configuration["EmailSettings:FromEmail"], configuration["EmailSettings:Password"]);

            // Send the email message asynchronously.
            await client.SendAsync(emailMessage);

            // Disconnect from the SMTP server after sending the email.
            await client.DisconnectAsync(true);
        }
    }
}
