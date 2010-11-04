using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace ePortafolioMVC.Helpers
{
    public static class EmailHelper
    {
            static String myAccount = "info.ePSE@gmail.com";
            static String myPass = "3p5e@p4S5_w0rD!";


            public static bool SendMail(String to, String subject, String message)
        {
            try
            {
                NetworkCredential loginInfo = new NetworkCredential(myAccount, myPass);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(myAccount);
                msg.To.Add(new MailAddress(to));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
                client.Send(msg);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
