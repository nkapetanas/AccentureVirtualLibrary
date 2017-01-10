using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
   public class HistoryDTO
    {
        public decimal HistoryId { get; set; }
        public Nullable<decimal> CopyId { get; set; }
        public Nullable<System.DateTime> BookedDay { get; set; }
        public Nullable<System.DateTime> ReturnDay { get; set; }
        public string Id { get; set; }

        public virtual Copy Copy { get; set; }
        public virtual AspNetUser User { get; set; }

    }
}
