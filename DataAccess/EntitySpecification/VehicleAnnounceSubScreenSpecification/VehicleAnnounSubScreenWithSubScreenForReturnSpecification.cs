using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.VehicleAnnounceSubScreenSpecification
{
    public class VehicleAnnounSubScreenWithSubScreenForReturnSpecification:BaseSpecification<VehicleAnnounceSubScreen>
    {
        public VehicleAnnounSubScreenWithSubScreenForReturnSpecification(int vehicleAnnounceSubSCreenId)
        :base(x=>x.Id==vehicleAnnounceSubSCreenId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}