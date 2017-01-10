using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;
using Microsoft.WindowsAzure.Storage.Table.Queryable;
using System.Web.Http;
using System.Linq.Expressions;

namespace BusLayerDataAccess.Services
{
    public class BooksService
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<BookDTO> getBooks() // 1.
        {
            var books = db.Books.Select(b => new BookDTO()
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                OriginTitle = b.OriginTitle,
                Description = b.Description,
                PublishYear = b.PublishYear,
                Pages = b.Pages,
                ISBN = b.ISBN,
                ISBNSet = b.ISBNSet,
                LastModified = b.LastModified,
                //AuthorName = b.Author.Name,
                //CategoryName = b.
                PublisherName = b.Publisher.Name

                //ola ta alla pedia


            }).ToList();

            return books;
        }


        public List<BookDTO> getBooks(string isbn) // 1.1
        {  // lista me ola ta vivlia me ISBN sigkekgimeno
            var books = db.Books.Where(o => o.ISBN.Equals(isbn)).Select(b => new BookDTO()
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                OriginTitle = b.OriginTitle,
                Description = b.Description,
                PublishYear = b.PublishYear,
                Pages = b.Pages,
                ISBN = b.ISBN,
                ISBNSet = b.ISBNSet,
                LastModified = b.LastModified,

                //CategoryName = b.
                PublisherName = b.Publisher.Name

                //ola ta alla pedia


            }).ToList();

            return books;
        }

        public List<BookDTO> getBooksByTitleKeywords(string[] keywords) // 1.1
        {
            for (int i = 0; i < keywords.Length; i++)
            {
                keywords[i] = keywords[i].ToLower();
            }



            var books = db.Books.Where(book => keywords.All(word => book.Title.ToLower().Contains(word))).Select(b => new BookDTO()
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                OriginTitle = b.OriginTitle,
                Description = b.Description,
                PublishYear = b.PublishYear,
                Pages = b.Pages,
                ISBN = b.ISBN,
                ISBNSet = b.ISBNSet,
                LastModified = b.LastModified,
                AuthorName = db.BookAuthors.Where(c => c.BookId == b.BookId).FirstOrDefault().Author.Name,
                //CategoryName = b.
                PublisherName = b.Publisher.Name

                //ola ta alla pedia


            });

            return books.ToList();
        }

        public List<BookDTO> getBooksByPublishYear(int publishYear) // 1.
        {  // lista me ola ta vivlia me ISBN sigkekgimeno
            var books = db.Books.Where(o => o.PublishYear == publishYear).Select(b => new BookDTO()
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                OriginTitle = b.OriginTitle,
                Description = b.Description,
                PublishYear = b.PublishYear,
                Pages = b.Pages,
                ISBN = b.ISBN,
                ISBNSet = b.ISBNSet,
                LastModified = b.LastModified,
                //AuthorName = b.Author.Name,
                //CategoryName = b.
                PublisherName = b.Publisher.Name

                //ola ta alla pedia


            });

            return books.ToList();
        }

        //public List<BookDTO> getBooksByPublisherName(string publisherName) // 1.2
        //{  // lista me ola ta vivlia me publisher sigkekgimeno
        //    var books = db.Books.Where(o => o.Publisher.Equals(publisherName)).Select(b => new BookDTO()
        //    {
        //        BookId = b.BookId,
        //        Title = b.Title,
        //        SubTitle = b.SubTitle,
        //        OriginTitle = b.OriginTitle,
        //        Description = b.Description,
        //        PublishYear = b.PublishYear,
        //        Pages = b.Pages,
        //        ISBN = b.ISBN,
        //        ISBNSet = b.ISBNSet,
        //        LastModified = b.LastModified,
        //        //AuthorName = b.Author.Name,
        //        //CategoryName = b.
        //        PublisherName = b.Publisher.Name

        //        //ola ta alla pedia


        //    }).ToList();

        //    return books;
        //}

        public BookDTO getBook(decimal id) // 2.
        {
            // psaxnei gia ena vivlio sigkekrimena me vasi to id tou
            var books = db.Books.Where(o => o.BookId == id).Select(b => new BookDTO()
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                OriginTitle = b.OriginTitle,
                Description = b.Description,
                PublishYear = b.PublishYear,
                Pages = b.Pages,
                ISBN = b.ISBN,
                ISBNSet = b.ISBNSet,
                LastModified = b.LastModified,
                AuthorName = db.BookAuthors.Where(c => c.BookId == id).FirstOrDefault().Author.Name,
                PublisherName = db.Publishers.FirstOrDefault().Name,







                //ola ta alla pedia


            });

            return books.FirstOrDefault();
        }

        public BookDTO getBook(string title) // 2.
        {
            // psaxnei gia ena vivlio sigkekrimena me vasi to titlo tou
            var books = db.Books.Where(o => o.Title == title).Select(b => new BookDTO()
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                OriginTitle = b.OriginTitle,
                Description = b.Description,
                PublishYear = b.PublishYear,
                Pages = b.Pages,
                ISBN = b.ISBN,
                ISBNSet = b.ISBNSet,
                LastModified = b.LastModified,
                // AuthorName = b.Author.Name,
                // CategoryName = b.
                PublisherName = b.Publisher.Name




                //ola ta alla pedia


            })/*.ToList()*/;

            return books.FirstOrDefault();
        }



        public int putBook(decimal id, BookDTO book) //3. update a book 
        {       // kane update 
            try
            {
                if (!BookExists(id)) { return -1; }
                var b = (from x in db.Books
                         where x.BookId == id
                         select x).First();
                b.BookId = book.BookId;
                b.Title = book.Title;
                b.SubTitle = book.SubTitle;
                b.OriginTitle = book.OriginTitle;
                b.Description = book.Description;
                b.PublishYear = book.PublishYear;
                b.Pages = book.Pages;
                b.ISBN = book.ISBN;
                b.ISBNSet = book.ISBNSet;
                b.LastModified = book.LastModified;
                // AuthorName = b.Author.Name,
                // CategoryName = b.
                b.Publisher.Name = book.PublisherName;



                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        public int postBook(BookDTO book)
        { //4. insert a book
            // kane insert
            try
            {

                //New Code
                var dto = new Book()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    SubTitle = book.SubTitle,
                    OriginTitle = book.OriginTitle,
                    Description = book.Description,
                    PublishYear = book.PublishYear,
                    ISBN = book.ISBN,
                    ISBNSet = book.ISBNSet,
                    Pages = book.Pages,
                    PublisherId = book.PublisherId,
                    LastModified = book.LastModified,
                    //AuthorName= book.AuthorName,
                    // CategoryName =b.Category.NameOfCategory,
                    // Publisher = book.PublisherName,


                };
                db.Books.Add(dto);
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int deleteBook(decimal id)
        {

            // delete a book 
            try
            {
                if (!BookExists(id)) { return -1; }

                var bookTobeDeleted = (from d in db.Books
                                       where d.BookId == id
                                       select d).Single();

                db.Books.Remove(bookTobeDeleted);
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

        private bool BookExists(decimal id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}