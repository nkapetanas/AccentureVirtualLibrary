using BusLayerDataAccess.ModelsDTO;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using BusLayerDataAccess.Services;
using AngularJSAuthentication.API;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/reservations")]
    public class ReservationsController : ApiController
    {
        [Route("{userName}")]
        [HttpGet]
        [Authorize(Roles = "LibraryUser")]
        public List<CopyDTO> GetUserReservations(string userName)
        {
            CheckUsernameForLibraryUser(userName);
            string userId = FindUserId(userName);


            var reservationService = new ReservationsServices();
            var reservation = reservationService.getReservation(userId);
            if (reservation.Count == 0) throw new HttpResponseException(HttpStatusCode.NotFound);

            var copies = new List<CopyDTO>();
            var copyService = new CopiesService();
            foreach (var r in reservation)
            {
                var copy = copyService.getCopybyId(r.CopyId);
                copies.Add(copy);
            }
            return copies;

        }

        [Route("{userName}")]
        [HttpPost]
        [Authorize(Roles = "LibraryUser")]
        public IHttpActionResult PostReservation(string userName, decimal copyId)
        {    // na kanei mia anazitisi me ta copy , na vriskw mia copy id gia na 3erei mprostga ta stoixiea tou vivliou 
            CheckUsernameForLibraryUser(userName);
            string userId = FindUserId(userName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservationService = new ReservationsServices();
            var result = reservationService.postReservation(userId, copyId);
            if (result != 1) return BadRequest();
            var copyService = new CopiesService();
            var copy = copyService.getCopybyId(copyId);
            return Ok(copy);
        }

        [Route("{userName}/{copyId}")]
        [HttpDelete]
        [Authorize(Roles = "LibraryUser")]
        public IHttpActionResult DeleteReservation(string userName, decimal copyId)
        {
            CheckUsernameForLibraryUser(userName);
            string userId = FindUserId(userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //This controller needs to check that the specific reservationId corresponds to user userName, otherwise throw unauthorized error (see CheckUsernameForLibraryUser for implementation)
            var reservationService = new ReservationsServices();
            var result = reservationService.deleteReservation(userId, copyId);


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

            return new AuthRepository().FindId(userName);
        }

        /*
         *  
         *  public List<ReservationDTO> GetReservations()
        {
            var reservationService = new ReservationsServices();
            return reservationService.getReservations();
        }

        public IHttpActionResult GetReservation(decimal id)
        {
            var reservationService = new ReservationsServices();
            var reservation = reservationService.getReservation(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        public IHttpActionResult PutReservation(decimal id, ReservationDTO reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservationService = new ReservationsServices();
            var result = reservationService.putReservation(id, reservation);

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


        public IHttpActionResult PostReservation(ReservationDTO reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservationService = new ReservationsServices();
            var result = reservationService.postReservation(reservation);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }

            //return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationsId }, reservation);
        }
        */







    }
}