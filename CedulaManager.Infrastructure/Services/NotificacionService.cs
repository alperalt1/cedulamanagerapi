using CedulaManager.Domain.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CedulaManager.Infrastructure.Services
{
    public class NotificacionService: INotificacionService
    {
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromEmail;

        public NotificacionService(string smtpUser, string smtpPass, string fromEmail)
        {
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
            _fromEmail = fromEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sistema de Cedula", _fromEmail));
                message.To.Add(new MailboxAddress("Usuario", to));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.mailersend.net", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mensaje: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalle técnico: {ex.InnerException.Message}");
                }
            }
        }
    }
}
