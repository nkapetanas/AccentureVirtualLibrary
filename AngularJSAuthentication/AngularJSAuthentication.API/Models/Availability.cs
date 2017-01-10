using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class BookAvailability
    {
        public String LibraryName { get; set; }
        public String Availability { get; set; }

        public int CopyId { get; set; }
    }
}