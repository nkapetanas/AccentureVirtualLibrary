using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;

namespace BusLayerDataAccess.Services
{
   public class HistoriesServices
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<HistoryDTO> getHistories() // 1.
        {
            var histories = db.Histories.Select(b => new HistoryDTO()
            {
                HistoryId = b.HistoryId,
                CopyId= b.CopyId,
                BookedDay = b.BookedDay,
                ReturnDay = b.ReturnDay,
                Id = b.Id,

                Copy = b.Copy,
                User = b.AspNetUser,


            }).ToList();

            return histories;
        }

        public HistoryDTO getHistory(decimal id) // 2.
        {
            // psaxnei gia ena vivlio sigkekrimena me vasi to id tou
            var histories = db.Histories.Where(o => o.HistoryId == id).Select(b => new HistoryDTO()
            {
                HistoryId = b.HistoryId,
                CopyId = b.CopyId,
                BookedDay = b.BookedDay,
                ReturnDay = b.ReturnDay,
                Id = b.Id,

                Copy = b.Copy,
                User = b.AspNetUser,



                //ola ta alla pedia


            })/*.ToList()*/;

            return histories.FirstOrDefault();
        }

        public int putHistory(decimal id, HistoryDTO history) //3. update a book 
        {       // kane update 
            try
            {
                if (!HistoryExists(id)) { return -1; }
                var b = (from x in db.Histories
                         where x.HistoryId == id
                         select x).First();
                b.HistoryId = history.HistoryId;
                b.CopyId = history.CopyId;
                b.BookedDay = history.BookedDay;
                b.ReturnDay = history.ReturnDay;
                b.Id = history.Id;

                b.Copy = history.Copy;
                b.AspNetUser = history.User;



                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        public int postHistory(HistoryDTO history)
        { //4. insert a HISTORY
            // kane insert
            try
            {

                //New Code
                var dto = new History()
                {
                HistoryId = history.HistoryId,
                CopyId = history.CopyId,
                BookedDay = history.BookedDay,
                ReturnDay = history.ReturnDay,
                Id = history.Id,

                Copy = history.Copy,
                AspNetUser = history.User,

            };
                db.Histories.Add(dto);
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int deleteHistory(decimal id)
        {

            // delete a book 
            try
            {
                if (!HistoryExists(id)) { return -1; }

                var historyTobeDeleted = (from d in db.Histories
                                       where d.HistoryId == id
                                       select d).Single();

                db.Histories.Remove(historyTobeDeleted);
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

        private bool HistoryExists(decimal id)
        {
            return db.Histories.Count(e => e.HistoryId == id) > 0;
        }

    }
}

