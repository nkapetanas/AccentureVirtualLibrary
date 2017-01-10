using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.Services;
using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess;

namespace WebAPI.Controllers
{
    public class AuthorsController : ApiController
    {
        

        // GET: api/Authors
        public List<AuthorDTO> GetAuthors()// 1.
        {
            var authorService = new AuthorsService();
            return authorService.getAuthors();

            // paei sto service kai sou girizei mia lista me tous Authors
        }

        // GET: api/Authors/5

        public  IHttpActionResult GetAuthor(decimal id)//2.
        {
            var authorService = new AuthorsService();
            var author = authorService.getAuthor(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        
        public IHttpActionResult PutAuthor(decimal id, AuthorDTO author) //3.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorService = new AuthorsService();
            var result = authorService.putAuthor(id,author);

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

        // POST: api/Authors
      
        public IHttpActionResult PostAuthor(AuthorDTO author)// 4.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorService = new AuthorsService();
            var result = authorService.postAuthor(author);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }


           // return CreatedAtRoute("DefaultApi", new { id = author.AuthorId }, author);
        }

        // DELETE: api/Authors/5
       
        public IHttpActionResult DeleteAuthor(decimal id)// 5.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorService = new AuthorsService();
            var result = authorService.deleteAuthor(id);




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

       
    }
}