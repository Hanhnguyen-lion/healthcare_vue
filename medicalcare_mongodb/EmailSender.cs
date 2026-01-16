
using System.Net;
using System.Net.Mail;

namespace medicalcare_mongodb
{
    public class EmailSender{
        public static void SendEmailAsync(
            string fromEmail, 
            string fromName, 
            string toEmail, 
            string subject, 
            string body, 
            string smtpHost, 
            int smtpPort, 
            string username, 
            string password)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("noreply@hcdpartners.com", "hIeS!laM81");
                // var client = new SmtpClient("smtp.gmail.com", 587)
                // {
                //     Credentials = new NetworkCredential("hanhnguyen.lion@gmail.com", "R2d2@Wru#4321"),
                //     EnableSsl = true
                // };
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                using (var mail = new MailMessage())
                {
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress("noreply@hcdpartners.com", "test");
                    mail.To.Add("hanhnguyen.lion@gmail.com");
                    mail.Subject = "Test";
                    mail.Body = "Test";
                    client.Send(mail);
                }

                // client.Send("hanhnguyen.lion@gmail.com", "hanhnguyen.lion@gmail.com", "test", "testbody");
            }

            // var message = new MimeMessage();
            // Console.WriteLine("fromName:"+fromName);
            // Console.WriteLine("fromEmail:"+fromEmail);
            // message.From.Add(new MailboxAddress(fromName, fromEmail));
            // message.To.Add(MailboxAddress.Parse(toEmail));
            // message.Subject = subject;
            // message.Body = new TextPart(MimeKit.Text.TextFormat.Html){Text=body};
            // using (var client = new SmtpClient())
            // {
            //     await client.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            //     await client.AuthenticateAsync(username, password);
            //     await client.SendAsync(message);
            //     await client.DisconnectAsync(true);
            // }
        }
        public static void SendEmail(
            string fromEmail, 
            string toEmail, 
            string subject, 
            string body, 
            string smtpHost, 
            int smtpPort, 
            string username, 
            string password)
        {
            Console.WriteLine("fromEmail:"+fromEmail);
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = smtpHost;
                client.Port = smtpPort;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(fromEmail, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                using (var mail = new MailMessage())
                {
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress(fromEmail, username);
                    mail.To.Add(new MailAddress(toEmail));
                    mail.Subject = subject;
                    mail.Body = body;
                    client.Send(mail);
                }
            }
        }
    }
}