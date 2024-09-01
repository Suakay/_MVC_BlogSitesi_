using BlogMainStructure.Business.DTOs.MailDTOs;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.Extensions.Options;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.MailServices
{
    public class MailService : IMailService
    {
        // Holds the SMTP settings.
        private readonly SmtpSettingsDTO _smtpSettings;

        // Constructor to inject SMTP settings through dependency injection.
        public MailService(IOptions<SmtpSettingsDTO> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        // Summary: 
        // This method sends an email asynchronously using the provided recipient email address, subject, and message body.
        // Parameters:
        // - mail: The recipient's email address.
        // - subject: The subject of the email.
        // - message: The body content of the email in HTML format.
        public async Task SendMailAsync(string mail, string subject, string message)
        {
            try
            {
                // Create a new email message.
                var newEmail = new MimeMessage();

                // Set the sender's email address.
                newEmail.From.Add(MailboxAddress.Parse("ADD-YOUR-OWN-EMAIL-ADDRESS"));

                // Set the recipient's email address.
                newEmail.To.Add(MailboxAddress.Parse(mail));

                // Set the email subject.
                newEmail.Subject = subject;

                // Create the body of the email.
                var builder = new BodyBuilder();
                builder.HtmlBody = message; // Set the HTML content.
                newEmail.Body = builder.ToMessageBody(); // Convert the body to a MimeMessage body format.

                // Connect to the SMTP server and send the email.
                using (var client = new SmtpClient())
                {
                    // Connect to the SMTP server using secure TLS options.
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);

                    // Authenticate using the sender's credentials.
                    await client.AuthenticateAsync(_smtpSettings.SenderName, _smtpSettings.Password);

                    // Send the email.
                    await client.SendAsync(newEmail);

                    // Disconnect from the SMTP server.
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // If an error occurs during the email sending process, throw a custom exception with the error message.
                throw new InvalidOperationException($"An error occurred while sending the email: " + ex.Message);
            }
        }
    }
}
