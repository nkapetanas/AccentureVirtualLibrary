using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLayerDataAccess.ModelsDTO
{
    
        public class BookDTO
        {
            public decimal BookId { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }
            public string OriginTitle { get; set; }
            public string Description { get; set; }
            public Nullable<int> PublishYear { get; set; }
            public Nullable<int> Pages { get; set; }
            public string ISBN { get; set; }
            public string ISBNSet { get; set; }
            public decimal PublisherId { get; set; }
            public Nullable<System.DateTime> LastModified { get; set; }
            public string AuthorName { get; set; }
           // public string CategoryName { get; set; }
            public string PublisherName { get; set; }

        //public virtual Publisher Publisher { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BookCategory> BookCategories { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Copy> Copies { get; set; }

    }
    
}
