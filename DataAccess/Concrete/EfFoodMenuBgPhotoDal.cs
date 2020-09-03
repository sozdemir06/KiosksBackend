using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete
{
    public class EfFoodMenuBgPhotoDal:EfEntityRepositoryBase<FoodMenuBgPhoto,DataContext>,IFoodMenuBgPhotoDal
    {
        
    }
}