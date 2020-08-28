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
    public class EfHomeAnnounceDal : EfEntityRepositoryBase<HomeAnnounce, DataContext>, IHomeAnnounceDal
    {
        public async Task<List<HomeAnnounce>> GetHomeAnnouncesForKiosksByScreenIdAsync(int screenId)
        {
            using (var context = new DataContext())
            {
                var dateNow = DateTime.Now;

                var homeAnnounces = await context.HomeAnnounces
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.HomeAnnouncePhotos)
                                .Include(s => s.HomeAnnounceSubScreens)
                                .Include(x => x.NumberOfRoom)
                                .Include(x => x.Heatingtype)
                                .Include(x => x.FlatOfHome)
                                .Include(x => x.BuildingAge)
                                .Where(x => x.HomeAnnounceSubScreens.Any(s => s.ScreenId == screenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return homeAnnounces;

            }
        }

        public async Task<List<HomeAnnounce>> GetHomeAnnouncesForKiosksBySubScreenIdAsync(int subScreenId)
        {
            using (var context = new DataContext())
            {
                var dateNow = DateTime.Now;

                var homeAnnounces = await context.HomeAnnounces
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.HomeAnnouncePhotos)
                                .Include(s => s.HomeAnnounceSubScreens)
                                .Where(
                                 x => x.HomeAnnounceSubScreens.Any(s => s.ScreenId == subScreenId) &&
                                 x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return homeAnnounces;
            }
        }
    }
}