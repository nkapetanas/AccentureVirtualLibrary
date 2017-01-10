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
//    public class BookCategoriesController : ApiController
//    {
//        private WebAPIContext db = new WebAPIContext();

//        // GET: api/BookCategories
//        public IQueryable<BookCategory> GetBookCategories()
//        {
//            return db.BookCategories;
//        }

//        // GET: api/BookCategories/5
//        [ResponseType(typeof(BookCategory))]
//        public async Task<IHttpActionResult> GetBookCategory(decimal id)
//        {
//            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
//            if (bookCategory == null)
//            {
//                return NotFound();
//            }

//            return Ok(bookCategory);
//        }

//        // PUT: api/BookCategories/5
//        [ResponseType(typeof(void))]
//        public async Task<IHttpActionResult> PutBookCategory(decimal id, BookCategory bookCategory)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != bookCategory.BookId)
//            {
//                return BadRequest();
//            }

//            db.Entry(bookCategory).State = EntityState.Modified;

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BookCategoryExists(id))
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

//        // POST: api/BookCategories
//        [ResponseType(typeof(BookCategory))]
//        public async Task<IHttpActionResult> PostBookCategory(BookCategory bookCategory)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.BookCategories.Add(bookCategory);

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (BookCategoryExists(bookCategory.BookId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtRoute("DefaultApi", new { id = bookCategory.BookId }, bookCategory);
//        }

//        // DELETE: api/BookCategories/5
//        [ResponseType(typeof(BookCategory))]
//        public async Task<IHttpActionResult> DeleteBookCategory(decimal id)
//        {
//            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
//            if (bookCategory == null)
//            {
//                return NotFound();
//            }

//            db.BookCategories.Remove(bookCategory);
//            await db.SaveChangesAsync();

//            return Ok(bookCategory);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool BookCategoryExists(decimal id)
//        {
//            return db.BookCategories.Count(e => e.BookId == id) > 0;
//        }
//    }
//}