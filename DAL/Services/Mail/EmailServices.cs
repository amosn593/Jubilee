using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DAL.Services.Mail;

public class EmailServices:IEmail
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    public EmailServices(IConfiguration configuration)
    {
        _smtpServer = configuration["SmtpSettings:Server"];
        _smtpPort = int.Parse(configuration["SmtpSettings:Port"]);
        _smtpUsername = configuration["SmtpSettings:Username"];
        _smtpPassword = configuration["SmtpSettings:Password"];
    }

    public async Task<string> SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jubilee", _smtpUsername));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };
            using var client = new SmtpClient();

            await client.ConnectAsync(_smtpServer, 587, false);
            await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return "Email sent successfully!";

        }
        catch(Exception ex)
        {
            return $"Email sending failed. Error: {ex.Message}";
        }
        
    }
    


}
