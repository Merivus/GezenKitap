using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

namespace GezenKitap.UI.Class
{
    public class Mail
    {
        const string MailUser = "gezenkitap@yandex.com";
        const string MailPass = "Ankara1.";

        private const string SMTP_SERVER = "http://schemas.microsoft.com/cdo/configuration/smtpserver";
        private const string SMTP_SERVER_PORT = "http://schemas.microsoft.com/cdo/configuration/smtpserverport";
        private const string SEND_USING = "http://schemas.microsoft.com/cdo/configuration/sendusing";
        private const string SMTP_USE_SSL = "http://schemas.microsoft.com/cdo/configuration/smtpusessl";
        private const string SMTP_AUTHENTICATE = "http://schemas.microsoft.com/cdo/configuration/smtpauthenticate";
        private const string SEND_USERNAME = "http://schemas.microsoft.com/cdo/configuration/sendusername";
        private const string SEND_PASSWORD = "http://schemas.microsoft.com/cdo/configuration/sendpassword";


#pragma warning disable CS0618 // Tür veya üye eski
        public MailMessage myMail { get; set; }
#pragma warning restore CS0618 // Tür veya üye eski

        public void SendMail(string to, string subject, string body)
        {
            myMail = new MailMessage();

            myMail.Fields[SMTP_SERVER] = "smtp.yandex.com";
            myMail.Fields[SMTP_SERVER_PORT] = 465;
            myMail.Fields[SEND_USING] = 2;
            myMail.Fields[SMTP_USE_SSL] = true;
            myMail.Fields[SMTP_AUTHENTICATE] = 1;
            myMail.Fields[SEND_USERNAME] = MailUser;
            myMail.Fields[SEND_PASSWORD] = MailPass;

            //MailMessage ePosta = new MailMessage();
            //ePosta.To.Add(to);
            //ePosta.Subject = subject;

            //ePosta.From = new MailAddress(MailUser);
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.yandex.com";
            //smtp.Credentials = new System.Net.NetworkCredential(MailUser, MailPass);
            //smtp.Port = 567;
            //smtp.EnableSsl = true;

            //smtp.Send(ePosta);

            myMail.From = MailUser;
            myMail.To = to;
            myMail.Subject = subject;
            myMail.Body = body;

            SmtpMail.Send(myMail);
        }
    }
}