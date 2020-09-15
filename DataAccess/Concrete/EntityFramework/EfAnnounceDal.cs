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
    public class EfAnnounceDal : EfEntityRepositoryBase<Announce, DataContext>, IAnnounceDal
    {
        public async Task<List<Announce>> GetAnnounceForKiosksByScreenIdAsync(int screenId)
        {
            using (var context = new DataContext())
            {
                var dateNow = DateTime.Now;

                var announces = await context.Announces
                                .Include(u => u.User)
                                .Include(p => p.AnnouncePhotos)
                                .Include(s => s.AnnounceSubScreens)
                                .Where(
                                 x => x.AnnounceSubScreens.Any(s => s.ScreenId == screenId) &&
                                 x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return announces;

            }
        }

        public async Task<List<Announce>> GetAnnounceForKiosksBySubScreenIdAsync(int subScreenId)
        {
            using (var context = new DataContext())
            {
                var dateNow = DateTime.Now;

                var announces = await context.Announces
                                .Include(u => u.User)
                                .Include(p => p.AnnouncePhotos)
                                .Include(s => s.AnnounceSubScreens)
                               .Where(x => x.AnnounceSubScreens.Any(s => s.SubScreenId == subScreenId) &&
                                 x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return announces;

            }
        }
    }
}