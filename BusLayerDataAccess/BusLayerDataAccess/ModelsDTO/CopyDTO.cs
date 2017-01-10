using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
    public class CopyDTO
    {
        public decimal CopyId { get; set; }
        public decimal BookId { get; set; }
        public string Location { get; set; }
        public Nullable<int> LocationInLibrary { get; set; }
        public Nullable<System.DateTime> DayOfBooking { get; set; }
        public Nullable<System.DateTime> ReturnDay { get; set; }
        public string Status { get; set; }
        public string Id { get; set; }
        public bool isReserved { get; set; }

    }
}
