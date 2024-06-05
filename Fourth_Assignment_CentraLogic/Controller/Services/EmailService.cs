using System.Net.Mail;
using System.Net;

namespace VisitorSecurityClearanceSystem.Services
{
    public class EmailService
    {

        
        public static bool SendEmail(string to, string from, string subject, string text)
        {
            bool flag = false;

            try
            {
                // Set up the SMTP client
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential("shaliniyadav.shivi@gmail.com", "adyuhvnnkipliuh")
                };

                // Create the email message
                var message = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = text
                };
                message.To.Add(new MailAddress(to));

                // Send the email
                smtpClient.Send(message);
                flag = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }

            return flag;
        }

    }
}
