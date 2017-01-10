using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.Services;
using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess;
using System;

namespace WebAPI.Controllers
{
    public class CopiesController : ApiController
    {
        [Authorize]//if you want to check the controller, comment this line out

        // GET: api/Copies
        public List<CopyDTO> GetCopies()
        {
            var copyService = new CopiesService();
            return copyService.getCopies();
        }


        // auto edw anti na kalite edw kalite apo ton Loans Controller apo tin methodo GetUserLoans
        //    public List<CopyDTO> GetLoansofUser(string userid)
        //{
        //    var copyService = new CopiesService();
        //    return copyService.getLoansofUser(userid);
        //}

        // GET: api/Copies/5

        //public IHttpActionResult GetCopy(decimal id)
        //{
        //    var copyService = new CopiesService();
        //    var copy = copyService.getCopy(id);

        //    if (copy == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(copy);
        //}

        //public List<CopyDTO> GetBooksLocationStatus(decimal id)
        //{
        //    var copyService = new CopiesService();
        //    return copyService.getBooksLocationStatus(id);
        //}

        // PUT: api/Copies/5


        // auto edw anti na kalite edw kalite apo ton Loans Controller apo tin methodo createLoan
        //public IHttpActionResult PutCopy(decimal id, CopyDTO copy)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var copyService = new CopiesService();
        //    var result = copyService.putCopy(id,copy);

        //    if (result == -1)
        //    {
        //        return NotFound();

        //    }
        //    else if (result == 0)
        //    {
        //        return BadRequest();

        //    }
        //    else { return Ok(); }

        //   // return StatusCode(HttpStatusCode.NoContent);
        //}


        // kalite apo tin LoansController sto Loanextend
        //public IHttpActionResult PutExtendCopyTime(decimal bookid, string userid, DateTime returnDay)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var copyService = new CopiesService();
        //    var result = copyService.putExtendCopyTime(bookid, userid, returnDay);

        //    if (result == -1)
        //    {
        //        return NotFound();

        //    }
        //    else if (result == 0)
        //    {
        //        return BadRequest();

        //    }
        //    else { return Ok(); }

        //    // return StatusCode(HttpStatusCode.NoContent);
        //}


        // POST: api/Copies

        public IHttpActionResult PostCopy(CopyDTO copy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var copyService = new CopiesService();
            var result = copyService.postCopy(copy);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }

           // return CreatedAtRoute("DefaultApi", new { id = copy.CopyId }, copy);
        }

        // DELETE: api/Copies/5

        public IHttpActionResult DeleteCopy(decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var copyService = new CopiesService();
            var result = copyService.deleteCopy(id);



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