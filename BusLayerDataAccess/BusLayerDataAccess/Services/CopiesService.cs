using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;

namespace BusLayerDataAccess.Services
{
    public class CopiesService
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<CopyDTO> getCopies() // 1.
        {
            var copies = db.Copies.Select(b => new CopyDTO()
            {
                CopyId = b.CopyId,

                BookId = b.BookId,
                Location = b.Location,
                LocationInLibrary = b.LocationInLibrary,
                DayOfBooking = b.DayOfBooking,
                ReturnDay = b.ReturnDay,
                Status = b.Status,
                Id = b.Id,


                //ola ta alla pedia


            }).ToList();

            return copies;
        }

        //public List<CopyDTO> getBooksLocationStatus(decimal id)
        //{
        //    var book_Library_Status = db.Copies.Where(o => o.BookId == id ).Select(b => new CopyDTO()
        //    {
        //        //CopyId = b.CopyId,

        //        //BookId = b.BookId,
        //        Location = b.Location,
        //        LocationInLibrary = b.LocationInLibrary,
        //        //DayOfBooking = b.DayOfBooking,
        //        //ReturnDay = b.ReturnDay,
        //        Status = b.Status,
        //        //Id = b.Id,


        //    }).ToList();

        //    return book_Library_Status;

        //}

        public List<CopyDTO> getLoansofUser(string userid)
        {
            var userloans_ReturnDay = db.Copies.Where(o => o.Id.Equals(userid)).Select(b => new CopyDTO()
            {
                CopyId = b.CopyId,

                BookId = b.BookId,
                Location = b.Location,
                LocationInLibrary = b.LocationInLibrary,
                //DayOfBooking = b.DayOfBooking,
                ReturnDay = b.ReturnDay,
                Status = b.Status,
                //Id = b.Id,


            }).ToList();

            return userloans_ReturnDay;

        }


        public CopyDTO getCopybyId(decimal id) // 2.
        {
            // psaxnei gia ena antigrafo sigkekrimena me vasi to id tou
            var copies = db.Copies.Where(o => o.CopyId == id).Select(b => new CopyDTO()
            {
                CopyId = b.CopyId,

                BookId = b.BookId,
                Location = b.Location,
                LocationInLibrary = b.LocationInLibrary,
                DayOfBooking = b.DayOfBooking,
                ReturnDay = b.ReturnDay,
                Status = b.Status,
                Id = b.Id,




                //ola ta alla pedia


            })/*.ToList()*/;

            return copies.FirstOrDefault();
        }

        public List<CopyDTO> GetCopiesOfBook(decimal bookId) // 2.
        {
            //try
            //{
                // psaxnei gia ena antigrafo sigkekrimena me vasi to id tou
                var copies = db.Copies.Where(o => o.BookId == bookId).Select(b => new CopyDTO()
                {
                    CopyId = b.CopyId,

                    BookId = b.BookId,
                    Location = b.Location,
                    LocationInLibrary = b.LocationInLibrary,
                    DayOfBooking = b.DayOfBooking,
                    ReturnDay = b.ReturnDay,
                    Status = b.Status,
                    isReserved = b.isReserved??false,
                    Id = b.Id,
                    //ola ta alla pedia
                });

                return copies.ToList();
            //}
            //catch
            //{
            //    return new List<CopyDTO>();
            //}
        }

        public int putCopy(string id, decimal copyid) //3. update a copy number 
        {       // kane update 
            try
            {
                if (!CopyExists(copyid)) { return -1; }
                var b = (from x in db.Copies
                         where x.CopyId == copyid
                         select x).First();

                //b.CopyId = copy.CopyId;
                //b.BookId = copy.BookId;
                //b.Location = copy.Location;
                //b.LocationInLibrary = copy.LocationInLibrary;

                if ((b.Id.Equals("0")) && (b.Status.Equals("Available")))
                {
                    DateTime today = DateTime.Now;
                    b.DayOfBooking = today;

                    b.ReturnDay = today.AddDays(7);
                    b.Status = "Not Available";
                    b.Id = id;
                    db.SaveChanges();
                    return 1;
                }

                return 0;

            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int putCopyForReturn(string id, decimal copyid) //3. update a copy number 
        {       // kane update 

            if (!CopyExists(copyid)) { return -1; }
            try
            {
                var b = (from x in db.Copies
                         where x.CopyId == copyid
                         where x.Id.Equals(id)
                         select x).First();
                b.DayOfBooking = null;

                b.ReturnDay = null;
                b.Id = "0";

                if (b.isReserved == null || (bool)!b.isReserved)
                {
                    b.Status = "Available";
                }

                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

            //b.CopyId = copy.CopyId;
            //b.BookId = copy.BookId;
            //b.Location = copy.Location;
            //b.LocationInLibrary = copy.LocationInLibrary;

        }

        public int putExtendCopyTime(string userid, decimal copyId) //3.
        {       // kane update 
            try
            {
                if (!CopyExists(copyId)) { return -1; }
                try
                {
                    var b = (from x in db.Copies
                             where x.CopyId == copyId
                             where x.Id.Equals(userid)
                             select x).First();
                    DateTime newExtendedDate = (DateTime)b.ReturnDay;
                    b.ReturnDay = newExtendedDate.AddDays(7);
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

        }

        public DateTime returnTheExtendedDate(string userid, decimal copyId)
        {
            try
            {
                DateTime? newExtendedDate = null;

                var b = (from x in db.Copies
                         where x.CopyId == copyId
                         where x.Id.Equals(userid)
                         select x).First();
                newExtendedDate = (DateTime)b.ReturnDay;
                return (DateTime)newExtendedDate;


            }
            catch (DbUpdateConcurrencyException)
            {
                DateTime? newExtendedDate = null;
                return (DateTime)newExtendedDate;
            }



        }



        public int postCopy(CopyDTO copy)
        { //4. insert a copy
            // kane insert
            try
            {

                //New Code
                var dto = new Copy()
                {
                    CopyId = copy.CopyId,
                    BookId = copy.BookId,
                    Location = copy.Location,
                    LocationInLibrary = copy.LocationInLibrary,
                    DayOfBooking = copy.DayOfBooking,
                    ReturnDay = copy.ReturnDay,
                    Status = copy.Status,
                    Id = copy.Id,



                };
                db.Copies.Add(dto);
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int deleteCopy(decimal id)
        {

            // delete a book 
            try
            {
                if (!CopyExists(id)) { return -1; }

                var copyTobeDeleted = (from d in db.Copies
                                       where d.CopyId == id
                                       select d).Single();

                db.Copies.Remove(copyTobeDeleted);
                db.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

        }

        private bool CopyExistsInReservations(decimal copyid)
        {
            return db.Reservations.Count(e => e.CopyId == copyid) > 0;
        }
        private bool CopyExists(decimal id)
        {
            return db.Copies.Count(e => e.CopyId == id) > 0;
        }
    }
}
