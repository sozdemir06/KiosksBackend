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
    public class EfNewsDal : EfEntityRepositoryBase<News, DataContext>, INewsDal
    {
        public async Task<List<News>> GetNewsForKiosksByScreenIdAsync(int screenId)
        {
            using (var context = new DataContext())
            {

                var news = await context.News
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.NewsPhotos)
                                .Include(s => s.NewsSubScreens)
                                .Where(x => x.NewsSubScreens.Any(s => s.ScreenId == screenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true || x.PublishStartDate >= DateTime.Now)
                                 .AsNoTracking()
                                 .ToListAsync();
                return news;

            }
        }

        public async Task<List<News>> GetNewsForKiosksBySubScreenIdAsync(int subScreenId)
        {
            using (var context = new DataContext())
            {

                var news = await context.News
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.NewsPhotos)
                                .Include(s => s.NewsSubScreens)
                                .Where(x => x.NewsSubScreens.Any(s => s.SubScreenId == subScreenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return news;

            }
        }

        public async Task<List<News>> GetNewsForPublicDto()
        {
            using (var context = new DataContext())
            {

                var news = await context.News
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.NewsPhotos)
                                .Where(x => x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return news;

            }
        }
    }
}