using Application.IServices;
using MimeKit;

namespace Application.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("souhailelghiouane98@gmail.com", "souhailelghiouane98@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            // Use the fully qualified name for the MailKit SmtpClient
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // Connect to the SMTP server
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with the SMTP server
                await client.AuthenticateAsync("souhailelghiouane98@gmail.com", "fpcz jyfw yyks hmsm ");

                // Send the email
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
