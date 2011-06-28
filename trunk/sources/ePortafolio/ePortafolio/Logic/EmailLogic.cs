using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace ePortafolio.Logic
{
    public class EmailLogic
    {

        static String myAccount = "info.ePSE@gmail.com";
        static String myPass = "3p5e@p4S5_w0rD!";

        public static bool SendMail(String to, String subject, String message)
        {
            return true;
            try
            {
                to = "murmmel@hotmail.com";

                var msg = new MailMessage(new MailAddress(myAccount),new MailAddress(to))
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(myAccount, myPass)
                };

                smtp.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
