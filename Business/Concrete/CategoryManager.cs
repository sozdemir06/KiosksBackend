using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;

        }
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
             var category=await categoryDal.GetAsync(x=>x.CategoryId==categoryId);
             if(category==null)
             {
                 throw new RestException(HttpStatusCode.BadRequest,new{NotFound=Messages.CategoryNotFound});
             }

             return category;
        }

        public async Task<List<Category>> GetCatgeoryListAsync()
        {
            var categories=await categoryDal.GetListAsync();
            if(categories.Count==0)
            {
                throw new RestException(HttpStatusCode.BadRequest,new{NotFound=Messages.CategoryNotFound});
            }

            return categories;
        }
    }
}