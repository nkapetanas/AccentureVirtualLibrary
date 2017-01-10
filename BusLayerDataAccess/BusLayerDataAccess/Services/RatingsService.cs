using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;
using Microsoft.WindowsAzure.Storage.Table.Queryable;
using System.Web.Http;

namespace BusLayerDataAccess.Services
{
   public class RatingsService
    {
       
            private EntityDBEntities1 db = new EntityDBEntities1();





            public int putRating(decimal bookid, float rating) //3. update a book 
            {       // kane update 
                try
                {
                    if (!RatingExists(bookid)) { return -1; }
                    var b = (from x in db.Ratings
                             where x.BookId == bookid
                             select x).First();
                    b.numberofRatings++;
                    b.sumofRatings += rating;

                    db.SaveChanges();
                    return 1;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return 0;
                }



            }

            public RatingsDTO getRating(decimal bookid)
            {
                try
                {
                    var rating = db.Ratings.Where(o => o.BookId == bookid).Select(b => new RatingsDTO()
                    {
                        sumofRatings = b.sumofRatings,
                        numberofRatings = b.numberofRatings,
                    });

                    return rating.FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }

            protected void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }

            }

            private bool RatingExists(decimal bookid)
            {
                return db.Ratings.Count(e => e.BookId == bookid) > 0;
            }




        }
    }

