using Microsoft.Extensions.Options;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using System.IO;
using MailKit.Net.Smtp;
using MailKit;

namespace Relmonitor.Repositories
{
    public class EmailRepo : IEmail
    {
        EmailSettings emailSet = null;

        public EmailRepo(IOptions<EmailSettings> options)
        {
            emailSet = options.Value;
        }

        public bool SendEmailCreate(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress("", emailSet.EmailID);
                emailMessage.From.Add(emailFrom);
                foreach(var toEmaill in emailData.EmailToID.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries))
                {
                    emailMessage.To.Add(new MailboxAddress("", toEmaill));
                }

                foreach (var ccEmaill in emailData.EmailCC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    emailMessage.Cc.Add(new MailboxAddress("", ccEmaill));
                }
                emailMessage.Subject = emailData.EmailSubject;
                string filePath = Directory.GetCurrentDirectory() + "\\MailTemplate\\CreateReleaseTemplate.html";
                string emailTemplateText = File.ReadAllText(filePath);
                emailTemplateText = string.Format(emailTemplateText, emailData.Application, emailData.Env, emailData.Date, emailData.Hour, 
                    emailData.Duration, emailData.Downtime, emailData.Contents, emailData.AddOn, emailData.Title, "REL");

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = emailTemplateText;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(emailSet.Host, emailSet.Port, emailSet.UseSSL);
                emailClient.Authenticate(emailSet.EmailID, emailSet.Pass);
                emailClient.Send(emailMessage);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool SendEmailStartEnd(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress("", emailSet.EmailID);
                emailMessage.From.Add(emailFrom);
                foreach (var toEmaill in emailData.EmailToID.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    emailMessage.To.Add(new MailboxAddress("", toEmaill));
                }

                foreach (var ccEmaill in emailData.EmailCC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    emailMessage.Cc.Add(new MailboxAddress("", ccEmaill));
                }
                emailMessage.Subject = emailData.EmailSubject;
                string filePath = "";
                if (emailData.Title.Contains("STARTING"))
                {
                    filePath = Directory.GetCurrentDirectory() + "\\MailTemplate\\SendStartEmail.html";
                }
                else
                {
                    filePath = Directory.GetCurrentDirectory() + "\\MailTemplate\\SendEndEmail.html";
                }
                string emailTemplateText = File.ReadAllText(filePath);
                emailTemplateText = string.Format(emailTemplateText, emailData.Application, emailData.Env, emailData.Date,
                    emailData.Duration, emailData.Downtime, emailData.Contents,emailData.Title);

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = emailTemplateText;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(emailSet.Host, emailSet.Port, emailSet.UseSSL);
                emailClient.Authenticate(emailSet.EmailID, emailSet.Pass);
                emailClient.Send(emailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
