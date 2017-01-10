using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.Services;
using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess;

namespace WebAPI.Controllers
{
    public class HistoriesController : ApiController
    {
        

        // GET: api/Histories
        public List<HistoryDTO> GetHistories()
        {
            var historyService = new HistoriesServices();
            return historyService.getHistories();
        }

        // GET: api/Histories/5
      
        public IHttpActionResult GetHistory(decimal id)
        {
            var historyService = new HistoriesServices();
            var history= historyService.getHistory(id);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // PUT: api/Histories/5
     
        public IHttpActionResult PutHistory(decimal id, HistoryDTO history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var historyService = new HistoriesServices();
            var result = historyService.putHistory(id , history);

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

        // POST: api/Histories
      
        public IHttpActionResult PostHistory(HistoryDTO history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var historyService = new HistoriesServices();
            var result = historyService.postHistory(history);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }


           // return CreatedAtRoute("DefaultApi", new { id = history.HistoryId }, history);
        }

        // DELETE: api/Histories/5
     
        public IHttpActionResult DeleteHistory(decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var historyService = new HistoriesServices();
            var result = historyService.deleteHistory(id);




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