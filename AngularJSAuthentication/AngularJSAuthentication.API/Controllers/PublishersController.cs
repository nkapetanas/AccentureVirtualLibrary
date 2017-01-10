using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.ModelsDTO;
using BusLayerDataAccess.Services;
using BusLayerDataAccess;


namespace WebAPI.Controllers
{
    public class PublishersController : ApiController
    {
        

        // GET: api/Publishers
        public List<PublisherDTO> GetPublishers()
        {
            var publisherService = new PublishersServices();
            return publisherService.getPublishers();

            // paei sto service kai sou girizei mia lista me ta PUBLISHERS
        }

        // GET: api/Publishers/5
      
        public IHttpActionResult GetPublisher(decimal id)
        {
            var publisherService = new PublishersServices();
            var publisher = publisherService.getPublisher(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        // PUT: api/Publishers/5
      
        public IHttpActionResult PutPublisher(decimal id, PublisherDTO publisher)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var publisherService = new PublishersServices();
            var result = publisherService.putPublisher(id, publisher);

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

        // POST: api/Publishers
        
        public IHttpActionResult PostPublisher(PublisherDTO publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisherService = new PublishersServices();
            var result = publisherService.postPublisher(publisher);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }

            //return CreatedAtRoute("DefaultApi", new { id = publisher.PublisherId }, publisher);
        }

        // DELETE: api/Publishers/5
        
        public IHttpActionResult DeletePublisher(decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisherService = new PublishersServices();
            var result = publisherService.deletePublisher(id);




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