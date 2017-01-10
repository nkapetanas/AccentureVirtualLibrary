using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;

namespace BusLayerDataAccess.Services
{
  public class PublishersServices
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<PublisherDTO> getPublishers() // 1.
        {
            var publishers = db.Publishers.Select(b => new PublisherDTO()
            {
                PublisherId = b.PublisherId,
                Name = b.Name,
                Address = b.Address,
                Email = b.Email,
                WebSite = b.WebSite,
                Phone1 = b.Phone1,
                Fax = b.Fax,
                LastModified = b.LastModified,
               
                //ola ta alla pedia


            }).ToList();

            return publishers;
        }

        public PublisherDTO getPublisher(decimal id) // 2.
        {
            // psaxnei gia ena publisher sigkekrimena me vasi to id tou
            var publishers = db.Publishers.Where(o => o.PublisherId == id).Select(b => new PublisherDTO()
            {
                PublisherId = b.PublisherId,
                Name = b.Name,
                Address = b.Address,
                Email = b.Email,
                WebSite = b.WebSite,
                Phone1 = b.Phone1,
                Fax = b.Fax,
                LastModified = b.LastModified,




                //ola ta alla pedia


            })/*.ToList()*/;

            return publishers.FirstOrDefault();
        }

        public int putPublisher(decimal id, PublisherDTO publisher) //3. update a book 
        {       // kane update 
            try
            {
                if (!PublisherExists(id)) { return -1; }
                var b = (from x in db.Publishers
                         where x.PublisherId == id
                         select x).First();
                b.PublisherId = publisher.PublisherId;
                b.Name = publisher.Name;
                b.Address = publisher.Address;
                b.Email = publisher.Email;
                b.WebSite = publisher.WebSite;
                b.Phone1 = publisher.Phone1;
                b.Fax = publisher.Fax;
                b.LastModified = publisher.LastModified;



                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        public int postPublisher(PublisherDTO publisher)
        { //4. insert a publisher
            // kane insert
            try
            {

                //New Code
                var dto = new Publisher()
                {
                    PublisherId = publisher.PublisherId,
                    Name = publisher.Name,
                    Address = publisher.Address,
                    Email = publisher.Email,
                    WebSite = publisher.WebSite,
                    Phone1 = publisher.Phone1,
                    Fax = publisher.Fax,
                    LastModified = publisher.LastModified,

            };
                db.Publishers.Add(dto);
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int deletePublisher(decimal id)
        {

            // delete a Publisher 
            try
            {
                if (!PublisherExists(id)) { return -1; }

                var publisherTobeDeleted = (from d in db.Publishers
                                       where d.PublisherId == id
                                       select d).Single();

                db.Publishers.Remove(publisherTobeDeleted);
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

        private bool PublisherExists(decimal id)
        {
            return db.Publishers.Count(e => e.PublisherId == id) > 0;
        }

    }
}
