using System;
using System.Net;
using System.Net.Mail;

namespace ClubSphere.Models
{
    public class EmailService
    {
        private readonly string fromEmail = "clubsphere4@gmail.com";
        private readonly string fromPassword = "lfbzfgedraiqjooe";

        public bool SendEmail(string toEmail, string subject, string body, out string errorMessage)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail, "ClubSphere");
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
