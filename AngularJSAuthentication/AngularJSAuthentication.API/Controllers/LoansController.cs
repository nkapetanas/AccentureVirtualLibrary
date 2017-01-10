using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BusLayerDataAccess;
using System.Security.Claims;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    //MariaController
    [RoutePrefix("api/loans")]
    public class LoansController : ApiController
    {
        IList<CopyDTO> copies = new CopyDTO[]
        {
            new CopyDTO() { CopyId=1, BookId=1, Location="Lib1", DayOfBooking=new DateTime(2016,11,27) },
            new CopyDTO() { CopyId=2, BookId=1, Location="Lib2", DayOfBooking=new DateTime(2016,11,28) },
            new CopyDTO() { CopyId=3, BookId=1, Location="Lib3", DayOfBooking=new DateTime(2016,11,29) }
        };


        [Route("{userName}")]
        [HttpGet]
        [Authorize(Roles = "LibraryUser, Librarian")]
        public List<CopyDTO> GetUserLoans(string userName)//you can change it to IHttpActionResult
        {
            CheckUsernameForLibraryUser(userName);
            string userId = FindUserId(userName);
            var copyService = new CopiesService();
            var loans= copyService.getLoansofUser(userId);
            if (loans.Count == 0) throw new HttpResponseException(HttpStatusCode.NotFound);
            return loans;
        }

        [Route("{userName}/{copyId}/renew")]
        [HttpPost]
        [Authorize(Roles = "LibraryUser,Librarian")]
        public IHttpActionResult RenewLoan(string userName, decimal copyId)
        {
            CheckUsernameForLibraryUser(userName);
            string userId = FindUserId(userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var copyService = new CopiesService();
            var result = copyService.putExtendCopyTime(userId, copyId);
            if (result == -1)
            {
                return NotFound();

            }
            else if (result == 0)
            {
                return BadRequest();

            }
            var newDate = copyService.returnTheExtendedDate(userId, copyId);
            return Ok(newDate);  // return date 
        }

        [Route("{userName}")]
        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public IHttpActionResult CreateLoan(string userName, decimal copyId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = FindUserId(userName);
            var copyService = new CopiesService();
            var result = copyService.putCopy(userId, copyId);
            var copy = copyService.getCopybyId(copyId);
            if (result == -1)
            {
                return NotFound();

            }
            else if (result == 0)
            {
                return BadRequest();

            }
            else { return Ok(copy); }
            // pigenw sto copies na allazw to copyid
        }

        [Route("{userName}/{copyId}")]
        [HttpDelete]
        [Authorize(Roles = "Librarian")]
        public IHttpActionResult ReturnCopy(string userName, decimal copyId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = FindUserId(userName);
            var copyService = new CopiesService();
            var result = copyService.putCopyForReturn(userId, copyId);



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


        private void CheckUsernameForLibraryUser(string userName)
        {
            string tokenUserName = User.Identity.Name;
            var identity = (ClaimsIdentity)User.Identity;
            string userRole = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value).SingleOrDefault();

            if (userRole.Equals("LibraryUser") && !userName.Equals(tokenUserName))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        private string FindUserId(string userName)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (User.Identity.Name.Equals(userName))
                return identity.Claims.Where(c => c.Type == "id")
                       .Select(c => c.Value).SingleOrDefault();
            var ans = new AuthRepository().FindId(userName);

            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(string.Format("No user with username = {0}", userName)),
                ReasonPhrase = "User not found"
            };

            if (ans == null) throw new HttpResponseException(resp);
            return ans;
        }

    }
}
