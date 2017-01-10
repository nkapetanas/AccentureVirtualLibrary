using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.API.Helpers
{
    public class EMailHelper
    {

        // Note: To send email you need to add actual email id and credential so that it will work as expected  
        public static readonly string EMAIL_SENDER = "accenturevirtuallibrary@outlook.com"; // change it to actual sender email id or get it from UI input  
        public static readonly string EMAIL_CREDENTIALS = "helloworld1234"; // Provide credentials   
        public static readonly string SMTP_CLIENT = "smtp-mail.outlook.com"; // as we are using outlook so we have provided smtp-mail.outlook.com   
        public static readonly string EMAIL_BODY = "Reset your Password <a href='http://{0}.safetychain.com/api/Account/forgotPassword?{1}'>Here.</a>";
        private string senderAddress;

        internal bool SendEMail(string emailId, object eMAIL_SUBJECT, string emailBody)
        {
            throw new NotImplementedException();
        }

        private string clientAddress;
        private string netPassword;

        public static object EMAIL_SUBJECT { get; internal set; }

        public EMailHelper(string sender, string Password, string client)
        {
            senderAddress = sender;
            netPassword = Password;
            clientAddress = client;
        }
        public bool SendEMail(string recipient, string subject, string message)
        {
            bool isMessageSent = false;
            //Intialise Parameters  
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(clientAddress);
            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(senderAddress, netPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new System.Net.Mail.MailMessage(senderAddress.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //System.Net.Mail.Attachment attachment;  
                //attachment = new Attachment(@"C:\Users\XXX\XXX\XXX.jpg");  
                //mail.Attachments.Add(attachment);  
                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                isMessageSent = false;
            }
            return isMessageSent;
        }
    }
}

