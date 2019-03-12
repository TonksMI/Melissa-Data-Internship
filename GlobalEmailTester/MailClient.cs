using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Mail;

namespace ConsoleApp15
{
    //Kevin Ubay-Ubay's code that sends emails of the Logs
    class MailClient
    {
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.Write("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }

        public void SendEmail(string subject, string destination, string messageBody, string sender, string test)
        {
            SmtpClient client = new SmtpClient("mail.melissadata.com");

            MailAddress from = new MailAddress(sender, $"Global Email Test {test}", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(destination);

            MailMessage message = new MailMessage(from, to);
            //message.Bcc.Add(new MailAddress("oscar@melissadata.com"));
            message.IsBodyHtml = true;
            message.Body = messageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string userState = "Sending " + subject;

            try
            {
                System.Console.WriteLine(MSG_PREFIX + "SENDING_EMAIL: " + subject);
                client.Send(message);
                message.Dispose();
                System.Console.WriteLine(MSG_PREFIX + "EMAIL SENT");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        private const string MSG_PREFIX = "MAIL_CLIENT: ";
    }
}
