using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSAuthentication.API.Models;
using System.Net.Mail;
using System.IO;

namespace AngularJSAuthentication.API.Controllers
{
    public class EmailController : ApiController
    {
        [HttpPost]
        [ActionName("sendmail")]
        public IHttpActionResult processAuthEmail(SendMailRequest mailModel)
        {

            // Send the email
            System.Net.Mail.MailMessage msg = new MailMessage();

            // Separate the recipient array
            string[] emailAddress = mailModel.recipient.Split(',');

            foreach (string currentEmailAddress in emailAddress)
            {
                msg.To.Add(new MailAddress(currentEmailAddress.Trim()));
            }

            // Separate the cc array , if not null
            string[] ccAddress = null;

            if (mailModel.cc != null)
            {
                ccAddress = mailModel.cc.Split(',');
                foreach (string currentCCAddress in ccAddress)
                {
                    msg.CC.Add(new MailAddress(currentCCAddress.Trim()));
                }
            }

            // Include the reply to if not null
            if (mailModel.replyto != null)
            {
                msg.ReplyToList.Add(new MailAddress(mailModel.replyto));
            }

            // Include the file attachment if the filename is not null
            if (mailModel.filename != null)
            {
                // Declare a temp file path where we can assemble our file
                string tempPath = Properties.Settings.Default["TempFile"].ToString();

                string filePath = Path.Combine(tempPath, mailModel.filename);

                using (System.IO.FileStream reader = System.IO.File.Create(filePath))
                {
                    byte[] buffer = Convert.FromBase64String(mailModel.filecontent);
                    reader.Write(buffer, 0, buffer.Length);
                    reader.Dispose();
                }

                msg.Attachments.Add(new Attachment(filePath));

            }

            string sendFromEmail = Properties.Settings.Default["accenturevirtuallibrary@outlook.com"].ToString();
            string sendFromName = Properties.Settings.Default["accenturevirtuallibrary"].ToString();
            string sendFromPassword = Properties.Settings.Default["helloworld1234"].ToString();

            msg.From = new MailAddress(sendFromEmail, sendFromName);
            msg.Subject = mailModel.subject;
            msg.Body = mailModel.body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            NetworkCredential cred = new System.Net.NetworkCredential(sendFromEmail, sendFromPassword);
            client.Credentials = cred;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            try
            {
                client.Send(msg);
                msg.Dispose();

                // Clean up the temp directory if used
                if (mailModel.filename != null)
                {
                    string tempPath = Properties.Settings.Default["TempFile"].ToString();
                    string filePath = Path.Combine(tempPath, mailModel.filename);
                    File.Delete(filePath);
                }

                return Ok("Mail Sent");
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
        //[HttpPost]
        //[AllowAnonymous]
        //public string PostSendGmail()
        //{
        //    SmtpClient client = new SmtpClient();
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.EnableSsl = true;
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;
        //    setup Smtp authentication
        //    System.Net.NetworkCredential credentials =
        //        new System.Net.NetworkCredential("accenturevirtuallibrary@gmail.com", "helloworld1234");
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = credentials;
        //    can be obtained from your model
        //    MailMessage msg = new MailMessage();
        //    msg.From = new MailAddress("accenturevirtuallibrary@gmail.com");
        //    msg.To.Add(new MailAddress("nkapetanas@hotmail.com"));

        //    msg.Subject = "Message from A.info";
        //    msg.IsBodyHtml = true;
        //    msg.Body = string.Format("<html><head></head><body><b>Message Email</b></body>");
        //    try
        //    {
        //        client.Send(msg);
        //        return "OK";
        //    }
        //    catch (Exception ex)
        //    {

        //        return "error:" + ex.ToString();
        //    }
        //}

    }
    }

