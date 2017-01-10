using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    public class ExampleController : ApiController
    {
        [Authorize (Roles = "LibraryUser")]
        // GET api/<controller>
        public String Get()
        {

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var Name = ClaimsPrincipal.Current.Identity.Name;
            var Name1 = User.Identity.Name;

            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value).SingleOrDefault();

            return User.Identity.Name;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}