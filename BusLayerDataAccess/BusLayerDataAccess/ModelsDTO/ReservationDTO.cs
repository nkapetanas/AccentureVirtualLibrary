using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
   public class ReservationDTO
    {
        public decimal ReservationsId { get; set; }
        public string Id { get; set; }
        public decimal CopyId { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<System.DateTime> ReservationDate { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }

        public virtual Copy Copy { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
