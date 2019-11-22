using System;
using System.Net;
using System.Net.Mail;

namespace BankMgmtSys
{
    public class Email
    {
        public static void SendEmail(string emailBody, string sendTo)
        {
            while (true)
            {
                string from = "";
                string password = "";

                // This one works only for gmail
                if (from.Contains("@gmail.com") || from.Contains("@outlook.com"))
                {
                    string bodyContent = emailBody;
                    string subject = "Account Info";
                    string to = sendTo;

                    if (bodyContent != null && subject != null)
                    {
                        try
                        {
                            EmailSend(to, password, from, subject, bodyContent);
                            Console.WriteLine("Email Sent!");
                            Console.WriteLine("Press Enter to continue...");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Credentials are Invalid.");
                            throw ex;
                        }
                        catch (SmtpException ex)
                        {
                            Console.WriteLine("Client took to long to respond or credentials are invalid.");
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong...");
                            throw ex;
                        }
                    }
                }
                break;
            }
            Console.Read();
            Console.Clear();
            MainMenu.ShowMenu();
        }

        /// <summary>
        /// Method used to send email to user
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="password">Sender Password</param>
        /// <param name="from">Sender</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="message">Email Body</param>
        private static void EmailSend(string to, string password, string from, string subject, string message)
        {
            SmtpClient client = new SmtpClient();
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = message;
            client.EnableSsl = true;
            client.Port = 587;

            client.Host = "smtp.gmail.com";
            //client.Host = "smtp.office365.com"; // IF Sender is using outlook un-comment this line and comment out previous line

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 50000;
            client.Send(msg);
        }
    }
}
