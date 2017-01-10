using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
   public class RatingsDTO
    {

        public decimal RatingsId { get; set; }
        public decimal BookId { get; set; }
        public Nullable<double> sumofRatings { get; set; }
        public Nullable<int> numberofRatings { get; set; }

        public virtual Book Book { get; set; }

    }
}
