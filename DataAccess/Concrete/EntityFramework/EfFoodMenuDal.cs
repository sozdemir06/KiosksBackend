using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFoodMenuDal : EfEntityRepositoryBase<FoodMenu, DataContext>, IFoodMenuDal
    {
        public async Task<List<FoodMenu>> GetFoodsMenuForKiosksByScreenIdAsync(int screenId)
        {
            using (var context = new DataContext())
            {

                var foodsMenu = await context.FoodMenus
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.FoodMenuPhotos)
                                .Include(s => s.FoodMenuSubScreens)
                                .Where(x => x.FoodMenuSubScreens.Any(s => s.ScreenId == screenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return foodsMenu;

            }
        }

        public async Task<List<FoodMenu>> GetFoodsMenuForKiosksBySubScreenIdAsync(int subScreenId)
        {
            using (var context = new DataContext())
            {

                var foodsMenu = await context.FoodMenus
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.FoodMenuPhotos)
                                .Include(s => s.FoodMenuSubScreens)
                                .Where(x => x.FoodMenuSubScreens.Any(s => s.SubScreenId == subScreenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return foodsMenu;

            }
        }
    }

}
