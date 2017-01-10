using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.Services;
using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess;

namespace WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        

        // GET: api/Users
        public List<UserDTO> GetUsers() // 1.
        {
            var userService = new UsersService();
            return userService.getUsers();

            // paei sto service kai sou girizei mia lista 
        }

        // GET: api/Users/5
       
        public IHttpActionResult GetUser(string id) // 2.
        {
            var userService = new UsersService();
            var user = userService.getUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public IHttpActionResult GetUser(string username, string id) // 2.
        {
            var userService = new UsersService();
            var user = userService.getUser(username,id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        // PUT: api/Users/5

        public IHttpActionResult PutUser(string id, UserDTO user) //3.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(user.Id))
            {
                return BadRequest();
            }

            var userService = new UsersService();
            var result = userService.putUser(id, user);

            if (result == -1)
            {
                return NotFound();

            }
            else if (result == 0)
            {
                return BadRequest();

            }
            else { return Ok(); }


            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        
        public IHttpActionResult PostUser(UserDTO user) //4.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userService = new UsersService();
            var result = userService.postUser(user);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }

           // return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        
        public IHttpActionResult DeleteUser(string id) // 5.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userService = new UsersService();
            var result = userService.deleteUser(id);



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