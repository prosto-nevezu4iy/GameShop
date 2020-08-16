using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            MailAddress to = new MailAddress(message.Destination);
            MailAddress from = new MailAddress("piotr@mailtrap.io");

            MailMessage msg = new MailMessage(from, to);
            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();

            return client.SendMailAsync(msg);
        }
    }
}
