using System.Net;
using System.Net.Mail;

namespace Blog.Services
{
    public class EmailService
    {
        public bool Send(string ToName,string toEmail,string subject,string body,string fromName = "Renato",string fromEmail = "rsnascimento159@gmail.com")
        {
            var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);
            smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.Username, Configuration.Smtp.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage()
            {
                From = new MailAddress(fromEmail,fromName),
                Subject = subject,
                Body = body,
                Priority = MailPriority.High,
                IsBodyHtml = true
            };
            mail.To.Add(new MailAddress(toEmail,ToName));

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }
    }
}
