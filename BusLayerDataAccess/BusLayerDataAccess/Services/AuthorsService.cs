using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;
using Microsoft.WindowsAzure.Storage.Table.Queryable;

namespace BusLayerDataAccess.Services
{
    public class AuthorsService
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<AuthorDTO> getAuthors() // 1.
        {
            var authors = db.Authors.Select(b => new AuthorDTO()
            {
                 AuthorId = b.AuthorId,
                 Name =b.Name,
                 Description = b.Description,
                Email = b.Email,
                WebSite = b.WebSite,
                LastModified = b.LastModified
                //ola ta alla pedia


            }).ToList();

            return authors;
        }

        public AuthorDTO getAuthor(decimal id) // 2.
        {
            var authors = db.Authors.Where(o => o.AuthorId == id).Select(b => new AuthorDTO()
            {
                AuthorId = b.AuthorId,
                Name = b.Name,
                Description = b.Description,
                Email = b.Email,
                WebSite = b.WebSite,
                LastModified = b.LastModified
                //ola ta alla pedia


            });

            return authors.FirstOrDefault(); 
        }

        public int putAuthor(decimal id, AuthorDTO author) //3. update a author 
        {       // kane update 
            try
            {
                if (!AuthorExists(id)) { return -1; }
                var b = (from x in db.Authors
                         where x.AuthorId == id
                         select x).First();
                b.AuthorId = author.AuthorId;
                b.Name = author.Name;
                b.Description = author.Description;
                b.Email = author.Email;
                b.WebSite = author.WebSite;
                b.LastModified = author.LastModified;




                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        public int postAuthor(AuthorDTO author) { //4. insert a author
            // kane insert
            try
            {

                //New Code
                var dto = new Author()
                {
                    AuthorId = author.AuthorId,
                    Name = author.Name,
                    Description = author.Description,
                    Email = author.Email,
                    WebSite = author.WebSite,
                    LastModified = author.LastModified


                };
                db.Authors.Add(dto);
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int deleteAuthor(decimal id)
        {

            // delete a book 
            try
            {
                if (!AuthorExists(id)) { return -1; }

                var authorTobeDeleted = (from d in db.Authors
                                       where d.AuthorId == id
                                       select d).Single();

                db.Authors.Remove(authorTobeDeleted);
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

        private bool AuthorExists(decimal id)
        {
            return db.Authors.Count(e => e.AuthorId == id) > 0;
        }
    }
}
