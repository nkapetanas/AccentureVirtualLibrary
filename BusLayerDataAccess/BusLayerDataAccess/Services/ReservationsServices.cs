
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using BusLayerDataAccess.ModelsDTO;

namespace BusLayerDataAccess.Services
{
    public class ReservationsServices
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<ReservationDTO> getReservations() // 1.
        {
            var reservation = db.Reservations.Select(b => new ReservationDTO()
            {
                ReservationsId = b.ReservationsId,
                CopyId = b.CopyId,
                Id = b.Id,
                Priority = b.Priority,
                ReservationDate = b.ReservationDate,
                LastModified = b.LastModified,

                Copy = b.Copy,
                User = b.AspNetUser,
                //ola ta alla pedia


            }).ToList();

            return reservation;
        }

        //public List<ReservationDTO> getReservationsofUser(string userid) // 1.
        //{
        //    var reservation = db.Reservations.Where(o => o.Id.Equals(userid)).Select(b => new ReservationDTO()
        //    {
        //        ReservationsId = b.ReservationsId,
        //        Id = b.Id,
        //        CopyId = b.CopyId,
        //        Priority = b.Priority,
        //        ReservationDate = b.ReservationDate,
        //        //LastModified = b.LastModified,

        //        Copy = b.Copy,
        //        User = b.AspNetUser,
        //        //ola ta alla pedia


        //    }).ToList();

        //    return reservation;
        //}



        public List<ReservationDTO> getReservation(string id) // 2.
        {
            // psaxnei gia ena reservation vivlio sigkekrimena me vasi to id tou
            var reservations = db.Reservations.Where(o => o.Id.Equals(id)).Select(b => new ReservationDTO()
            {
                ReservationsId = b.ReservationsId,
                Id = b.Id,
                CopyId = b.CopyId,
                Priority = b.Priority,
                ReservationDate = b.ReservationDate,
                LastModified = b.LastModified,

                //Copy = b.Copy,
                // User = b.AspNetUser,




                //ola ta alla pedia


            }).ToList();

            return reservations;
        }

        public int putReservation(decimal id, ReservationDTO reservation) //3. update a reservation 
        {       // kane update 
            try
            {
                if (!ReservationExists(id)) { return -1; }
                var b = (from x in db.Reservations
                         where x.ReservationsId == id
                         select x).First();
                b.ReservationsId = reservation.ReservationsId;
                b.Id = reservation.Id;
                b.CopyId = reservation.CopyId;
                b.Priority = reservation.Priority;
                b.ReservationDate = reservation.ReservationDate;
                b.LastModified = reservation.LastModified;

                b.Copy = reservation.Copy;
                b.AspNetUser = reservation.User;



                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        public int postReservation(string userId, decimal copyid)
        { //4. insert a reservation
          // kane insert

            Random rnd = new Random();
            DateTime today = DateTime.Now;
            //New Code
            try
            {
                var b = (from x in db.Copies
                         where x.CopyId == copyid
                         select x).First();
                if (b.isReserved != null && (bool)b.isReserved)
                {
                    return -1;
                }
                else
                {
                    var dto = new Reservation()
                    {
                        ReservationsId = rnd.Next(1, 100),
                        Id = userId,
                        CopyId = copyid,
                        //Priority = reservation.Priority,

                        ReservationDate = today,
                        LastModified = today,

                        //Copy = reservation.Copy,
                        // AspNetUser = reservation.User,

                    };
                    b.isReserved = true;
                    db.Reservations.Add(dto);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch { return 0; }


        }

        public int deleteReservation(string userId, decimal id)
        {

            // delete a book 
            try
            {
                //if (!ReservationExists(id)) { return -1; }

                var reservationTobeDeleted = (from d in db.Reservations
                                              where d.CopyId == id
                                              where d.Id.Equals(userId)
                                              select d).Single();

                db.Reservations.Remove(reservationTobeDeleted);

                var b = (from x in db.Copies
                         where x.CopyId == id
                         select x).First();
                b.isReserved = false;
                db.SaveChanges();
                return 1;

            }
            catch
            {
                return 0;
            }



        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

        }

        private bool ReservationExists(decimal id)
        {
            return db.Reservations.Count(e => e.ReservationsId == id) > 0;
        }

    }
}
