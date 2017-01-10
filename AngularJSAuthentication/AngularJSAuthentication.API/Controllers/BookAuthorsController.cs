//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Description;
//using WebAPI;
//using WebAPI.Models;

//namespace WebAPI.Controllers
//{
//    public class BookAuthorsController : ApiController
//    {
//        private WebAPIContext db = new WebAPIContext();

//        // GET: api/BookAuthors
//        public IQueryable<BookAuthor> GetBookAuthors()
//        {
//            return db.BookAuthors;
//        }

//        // GET: api/BookAuthors/5
//        [ResponseType(typeof(BookAuthor))]
//        public async Task<IHttpActionResult> GetBookAuthor(decimal id)
//        {
//            BookAuthor bookAuthor = await db.BookAuthors.FindAsync(id);
//            if (bookAuthor == null)
//            {
//                return NotFound();
//            }

//            return Ok(bookAuthor);
//        }

//        // PUT: api/BookAuthors/5
//        [ResponseType(typeof(void))]
//        public async Task<IHttpActionResult> PutBookAuthor(decimal id, BookAuthor bookAuthor)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != bookAuthor.BookId)
//            {
//                return BadRequest();
//            }

//            db.Entry(bookAuthor).State = EntityState.Modified;

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BookAuthorExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/BookAuthors
//        [ResponseType(typeof(BookAuthor))]
//        public async Task<IHttpActionResult> PostBookAuthor(BookAuthor bookAuthor)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.BookAuthors.Add(bookAuthor);

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (BookAuthorExists(bookAuthor.BookId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtRoute("DefaultApi", new { id = bookAuthor.BookId }, bookAuthor);
//        }

//        // DELETE: api/BookAuthors/5
//        [ResponseType(typeof(BookAuthor))]
//        public async Task<IHttpActionResult> DeleteBookAuthor(decimal id)
//        {
//            BookAuthor bookAuthor = await db.BookAuthors.FindAsync(id);
//            if (bookAuthor == null)
//            {
//                return NotFound();
//            }

//            db.BookAuthors.Remove(bookAuthor);
//            await db.SaveChangesAsync();

//            return Ok(bookAuthor);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool BookAuthorExists(decimal id)
//        {
//            return db.BookAuthors.Count(e => e.BookId == id) > 0;
//        }
//    }
//}