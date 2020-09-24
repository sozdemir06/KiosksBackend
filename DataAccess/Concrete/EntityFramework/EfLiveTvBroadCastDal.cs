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
    public class EfLiveTvBroadCastDal : EfEntityRepositoryBase<LiveTvBroadCast, DataContext>, ILiveTvBroadCastDal
    {
        public async Task<List<LiveTvBroadCast>> GetLiveTvBroadCastForKiosksByScreenIdAsync(int screenId)
        {
            using (var context = new DataContext())
            {
                var dateNow = DateTime.Now;

                var liveTvBroadCasts = await context.LiveTvBroadCasts
                                .Include(u => u.User)
                                .Include(s => s.LiveTvBroadCastSubScreens)
                                .Where(
                                 x => x.LiveTvBroadCastSubScreens.Any(s => s.ScreenId == screenId) &&
                                 x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true || x.PublishStartDate>=DateTime.Now)
                                 .AsNoTracking()
                                 .ToListAsync();
                return liveTvBroadCasts;

            }
        }

        public async Task<List<LiveTvBroadCast>> GetLiveTvBroadCastForKiosksBySubScreenIdAsync(int subScreenId)
        {
            using (var context = new DataContext())
            {
                var dateNow = DateTime.Now;

                var announces = await context.LiveTvBroadCasts
                                .Include(u => u.User)
                                .Include(s => s.LiveTvBroadCastSubScreens)
                               .Where(x => x.LiveTvBroadCastSubScreens.Any(s => s.SubScreenId == subScreenId) &&
                                 x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return announces;

            }
        }
    }
}