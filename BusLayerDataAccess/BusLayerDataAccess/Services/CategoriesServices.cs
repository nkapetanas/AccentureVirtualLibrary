using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;
using Microsoft.WindowsAzure.Storage.Table.Queryable;

namespace BusLayerDataAccess.Services
{
   public class CategoriesServices
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<CategoryDTO> getCategories() // 1.
        {
            var categories = db.Categories.Select(b => new CategoryDTO()
            {
                CategoryId = b.CategoryId,
                NameOfCategory =b.NameOfCategory


            }).ToList();

            return categories;
        }

        public CategoryDTO getCategory(decimal id) // 2.
        {
            var category = db.Categories.Where(o => o.CategoryId == id).Select(b => new CategoryDTO()
            {
                CategoryId = b.CategoryId,
                NameOfCategory = b.NameOfCategory
                //ola ta alla pedia


            });

            return category.FirstOrDefault();
        }

        public int putCategory(decimal id, CategoryDTO category) //3. update a author 
        {       // kane update 
            try
            {
                if (!CategoryExists(id)) { return -1; }

                var b = (from x in db.Categories
                         where x.CategoryId == id
                         select x).First();
                b.CategoryId = category.CategoryId;
                b.NameOfCategory = category.NameOfCategory;
                




                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            return 0;

        }

        public int postCategory(CategoryDTO category)
        { //4. insert a category
            // kane insert
            try
            {

                //New Code
                var dto = new Category()
                {
                    CategoryId = category.CategoryId,
                    NameOfCategory = category.NameOfCategory


                };
                db.Categories.Add(dto);
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

            //return 0;

        }

        public int deleteCategory(decimal id)
        {

            // delete a book 
            try
            {
                if (!CategoryExists(id)) { return -1; }

                var categoryTobeDeleted = (from d in db.Categories
                                         where d.CategoryId == id
                                         select d).Single();

                db.Categories.Remove(categoryTobeDeleted);
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
        private bool CategoryExists(decimal id)
        {
            return db.Categories.Count(e => e.CategoryId == id) > 0;
        }


    }
}
