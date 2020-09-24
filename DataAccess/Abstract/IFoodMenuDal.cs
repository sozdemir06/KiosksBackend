using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IFoodMenuDal : IEntityRepository<FoodMenu>
    {
        Task<List<FoodMenu>> GetFoodsMenuForKiosksByScreenIdAsync(int screenId);
        Task<List<FoodMenu>> GetFoodsMenuForKiosksBySubScreenIdAsync(int subScreenId);
        Task<List<FoodMenu>> GetFoodMenusForPublicAsync();
    }
}