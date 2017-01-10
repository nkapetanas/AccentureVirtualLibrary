using AngularJSAuthentication.API.Helpers;
using AngularJSAuthentication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    public class EmailNotifierController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SendEmailNotification(EmailInput data)
        {
            ResponseBase updateResponse = new ResponseBase();
            var updateRequest = new RequestBase<EmailInput>(data);
            try
            {
                EMailHelper mailHelper = new EMailHelper(EMailHelper.EMAIL_SENDER, EMailHelper.EMAIL_CREDENTIALS, EMailHelper.SMTP_CLIENT);
                var emailBody = String.Format(EMailHelper.EMAIL_BODY);
                if (mailHelper.SendEMail(data.EmailId, EMailHelper.EMAIL_SUBJECT, emailBody))
                {
                    //   
                }
            }
            catch (Exception ex)
            { }
            return Ok(updateResponse);
        }

        //private void sendEmailViaWebApi()
        //{
        //    string subject = "Email Subject";
        //    string body = "Email body";
        //    string FromMail = "shahid@reckonbits.com.pk";
        //    string emailTo = "reciever@reckonbits.com.pk";
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("mail.reckonbits.com.pk");
        //    mail.From = new MailAddress(FromMail);
        //    mail.To.Add(emailTo);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    SmtpServer.Port = 25;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("shahid@reckonbits.com.pk", "your password");
        //    SmtpServer.EnableSsl = false;
        //    SmtpServer.Send(mail);
        //}

    }
}
