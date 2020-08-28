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
    public class EfVehicleAnnounceDal : EfEntityRepositoryBase<VehicleAnnounce, DataContext>, IVehicleAnnounceDal
    {
        public async Task<List<VehicleAnnounce>> GetVehicleAnnouncesForKiosksByScreenIdAsync(int screenId)
        {
            using (var context = new DataContext())
            {

                var vehicleAnnounces = await context.VehicleAnnounces
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.VehicleAnnouncePhotos)
                                .Include(s => s.VehicleAnnounceSubScreens)
                                .Include(x => x.VehicleCategory)
                                 .Include(x => x.VehicleBrand)
                                .Include(x => x.VehicleModel)
                                .Include(x => x.VehicleFuelType)
                                .Include(x => x.VehicleEngineSize)
                                .Include(x => x.VehicleGearType)
                                .Where(x => x.VehicleAnnounceSubScreens.Any(s => s.ScreenId == screenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return vehicleAnnounces;

            }
        }

        public async Task<List<VehicleAnnounce>> GetVehicleAnnouncesForKiosksBySubScreenIdAsync(int subScreenId)
        {
            using (var context = new DataContext())
            {

                var vehicleAnnounces = await context.VehicleAnnounces
                                .Include(u => u.User)
                                .Include(x => x.User.Campus)
                                .Include(x => x.User.Degree)
                                .Include(x => x.User.Department)
                                .Include(p => p.VehicleAnnouncePhotos)
                                .Include(s => s.VehicleAnnounceSubScreens)
                                .Include(x => x.VehicleCategory)
                                  .Include(x => x.VehicleBrand)
                                .Include(x => x.VehicleModel)
                                .Include(x => x.VehicleFuelType)
                                .Include(x => x.VehicleEngineSize)
                                .Include(x => x.VehicleGearType)
                                .Where(x => x.VehicleAnnounceSubScreens.Any(s => s.SubScreenId == subScreenId)
                                 && x.PublishStartDate <= DateTime.Now && x.PublishFinishDate >= DateTime.Now && x.IsPublish == true)
                                 .AsNoTracking()
                                 .ToListAsync();
                return vehicleAnnounces;

            }
        }
    }
}