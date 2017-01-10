using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
   public class PublisherDTO
    {
        public decimal PublisherId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Phone1 { get; set; }
        public string Fax { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }

    }
}
