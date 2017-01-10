using System.Collections.Generic;
using System.Web.Http;
using BusLayerDataAccess.Services;
using BusLayerDataAccess.ModelsDTO;

namespace WebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        

        // GET: api/Categories
        public List<CategoryDTO> GetCategories() // 1.
        {
            var categoryService = new CategoriesServices();
            return categoryService.getCategories();

            // paei sto service kai sou girizei mia lista me tis catigories
        }

        // GET: api/Categories/5
        
        public IHttpActionResult GetCategory(decimal id)// 2.
        {
            var categoryService = new CategoriesServices();
            var category = categoryService.getCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        
        public IHttpActionResult PutCategory(decimal id, CategoryDTO category) // 3.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryService = new CategoriesServices();
            var result = categoryService.putCategory(id,category);

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

        // POST: api/Categories
        
        public IHttpActionResult PostCategory(CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryService = new CategoriesServices();
            var result = categoryService.postCategory(category);

            if (result != 0)
            {
                return Ok();

            }
            else { return BadRequest(); }

            //return CreatedAtRoute("DefaultApi", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        
        public IHttpActionResult DeleteCategory(decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryService = new CategoriesServices();
            var result = categoryService.deleteCategory(id);



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