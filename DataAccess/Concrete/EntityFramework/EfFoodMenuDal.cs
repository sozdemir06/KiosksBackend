using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFoodMenuDal:EfEntityRepositoryBase<FoodMenu,DataContext>,IFoodMenuDal
    {
        
    }
}