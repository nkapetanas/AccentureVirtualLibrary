using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
   public class AuthorDTO
    {
        public decimal AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }

    }
}
