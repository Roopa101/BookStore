using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailServices
    {
        public static void sendmail(string EmailId, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("jamadarroopa71@gmail.com", "roopaganu111");

                MailMessage msgobj = new MailMessage();
                msgobj.To.Add(EmailId);
                msgobj.From = new MailAddress("jamadarroopa71@gmail.com");
                msgobj.Subject = "password reset link";
                //msgobj.Body = $"BookStoreApplication/{token}";
                msgobj.Body = $"www.BookStoreApplication.com/reset-password/{token}";
                client.Send(msgobj);
            }
        }
    }
}
