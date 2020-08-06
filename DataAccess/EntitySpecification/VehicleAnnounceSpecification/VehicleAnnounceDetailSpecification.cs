using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.VehicleAnnounceSpecification
{
    public class VehicleAnnounceDetailSpecification:BaseSpecification<VehicleAnnounce>
    {
        public VehicleAnnounceDetailSpecification(int vehicleAnnounceId)
        :base(x=>x.Id==vehicleAnnounceId)
        {
            AddInclude(x=>x.VehicleAnnouncePhotos);
            AddInclude(x=>x.VehicleAnnounceSubScreens);
            AddInclude(x=>x.User);
            AddInclude(x=>x.User.Department);
            AddInclude(x=>x.User.Campus);
            AddInclude(x=>x.User.Degree);
            AddInclude(x=>x.VehicleBrand);
            AddInclude(x=>x.VehicleCategory);
            AddInclude(x=>x.VehicleModel);
            AddInclude(x=>x.VehicleFuelType);
            AddInclude(x=>x.VehicleEngineSize);
            AddInclude(x=>x.VehicleGearType);
        }
    }
}