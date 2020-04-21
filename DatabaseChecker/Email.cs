using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseChecker
{
    public enum EmailType
    {
        Info,
        Alert
    }

    public class Email
    {
        static Logger logger = new Logger();

        public void SendEmail(EmailType emailType, double maxSize, double currentSize)
        {
            DateTime dt = DateTime.Now;
            string emailAddresses = ConfigurationManager.AppSettings["emailAddresses"].ToString();
            string fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();

            string infoSubject = ConfigurationManager.AppSettings["infoSubject"].ToString();
            string infoBody = ConfigurationManager.AppSettings["infoBody"].ToString();

            string alertSubject = ConfigurationManager.AppSettings["alertSubject"].ToString();
            string alertBody = ConfigurationManager.AppSettings["alertBody"].ToString();

            string smtpConfig = ConfigurationManager.AppSettings["smtpConfig"].ToString();

            using (MailMessage mailMessage = new MailMessage(fromAddress, emailAddresses))
            {
                switch(emailType)
                {
                    case EmailType.Info:
                        mailMessage.Subject = infoSubject;
                        mailMessage.Body = infoBody;
                        break;
                    case EmailType.Alert:
                        mailMessage.Subject = alertSubject;
                        mailMessage.Body = alertBody;
                        break;
                    default:
                        break;
                }
                
                using(SmtpClient smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = true;
                    smtp.Host = smtpConfig;

                    try
                    {
                        smtp.Send(mailMessage);
                    }
                    catch(Exception ex)
                    {
                        logger.Log(LogType.Error, "Exception sending email", "SendMail()", ex.ToString());
                    }
                }
            }
        }
    }
}
