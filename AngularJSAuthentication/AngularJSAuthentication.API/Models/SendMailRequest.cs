using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class SendMailRequest
    {
        public string recipient { get; set; }
        public string cc { get; set; }
        public string replyto { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string filecontent { get; set; }
        public string filename { get; set; }

    }
}