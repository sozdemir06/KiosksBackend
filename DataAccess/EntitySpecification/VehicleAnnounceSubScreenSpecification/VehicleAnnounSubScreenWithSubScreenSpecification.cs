using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.VehicleAnnounceSubScreenSpecification
{
    public class VehicleAnnounSubScreenWithSubScreenSpecification:BaseSpecification<VehicleAnnounceSubScreen>
    {
        public VehicleAnnounSubScreenWithSubScreenSpecification(int vehicleAnnounceId)
        :base(x=>x.VehicleAnnounceId==vehicleAnnounceId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}