using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IVehicleAnnounceDal : IEntityRepository<VehicleAnnounce>
    {
        Task<List<VehicleAnnounce>> GetVehicleAnnouncesForKiosksByScreenIdAsync(int screenId);
        Task<List<VehicleAnnounce>> GetVehicleAnnouncesForKiosksBySubScreenIdAsync(int subScreenId);
    }
}