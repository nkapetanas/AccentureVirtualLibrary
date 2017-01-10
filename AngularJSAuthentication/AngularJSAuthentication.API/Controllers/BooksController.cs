using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.Services;
using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        [Route("")]
        [HttpGet]
        public List<BookDTO> GetBooks() // 1.
        {
            var bookService = new BooksService();
            return bookService.getBooks();

            // paei sto service kai sou girizei mia lista me ta vivlia
        }


        [Route("{bookId}/availability")]
        [HttpGet]
        public IHttpActionResult GetBookCopies(decimal bookId)
        {
            var copyService = new CopiesService();
            var copy = copyService.GetCopiesOfBook(bookId);

            if (copy.Count==0)
            {
                return NotFound();
            }

            return Ok(copy);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetBookByKeywords(string q)
        {
            string[] keywords = q.Split();
            var booksService = new BooksService();
            var books = booksService.getBooksByTitleKeywords(keywords);

            if (books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }


        [Route("{bookId}")]
        [HttpGet]
        public IHttpActionResult GetBook(decimal bookId) // 2.
        {
            var bookService = new BooksService();
            var book = bookService.getBook(bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Route("")]
        [HttpGet]
        public List<BookDTO> GetBooks(string isbn) // 1.1
        {
            var bookService = new BooksService();
            return bookService.getBooks(isbn);

            // paei sto service kai sou girizei mia lista me ta vivlia
        }

        [Route("")]
        [HttpGet]
        public List<BookDTO> GetBooksByPublishYear(int publishYear) // 1.1
        {
            var bookService = new BooksService();
            return bookService.getBooksByPublishYear(publishYear);

            // paei sto service kai sou girizei mia lista me ta vivlia
        }

        public IHttpActionResult PutBook(decimal id, BookDTO book) //3.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookService = new BooksService();
            var result = bookService.putBook(id, book);

            if (result == -1)
            {
                return NotFound();

            }
            else if (result == 0)
            {
                return BadRequest();

            }
            else { return Ok(); }


            // return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books

        public IHttpActionResult PostBook(BookDTO book)// 4.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookService = new BooksService();
            var result = bookService.postBook(book);
            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }



            //return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5

        public IHttpActionResult DeleteBook(decimal id)// 5.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookService = new BooksService();
            var result = bookService.deleteBook(id);



            if (result == -1)
            {
                return NotFound();

            }
            else if (result == 0)
            {
                return BadRequest();

            }
            else { return Ok(); }
        }

        [Route("{bookId}/rating")]
        [HttpGet]
        public IHttpActionResult GetBookRating(decimal bookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ratingService = new RatingsService();
            var rating = ratingService.getRating(bookId);
            if (rating.numberofRatings == 0) return Ok(-1);
            return Ok((float)(rating.sumofRatings / rating.numberofRatings));

        }

        [Route("{bookId}/rating")]
        [HttpPost]
        public IHttpActionResult PostBookRating(decimal bookId, float rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ratingService = new RatingsService();
            var status = ratingService.putRating(bookId, rating);
            if (status == 0) return BadRequest();

            var updatedRating = ratingService.getRating(bookId);

            return Ok((float)(updatedRating.sumofRatings / updatedRating.numberofRatings));
        }
    }

}