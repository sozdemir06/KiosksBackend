using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.VehicleAnnounceSpecification
{
    public class VehicleAnnounceByUserIdSpecification : BaseSpecification<VehicleAnnounce>
    {
        public VehicleAnnounceByUserIdSpecification(VehicleAnnounceParams queryParams, int userId):base(x=>x.UserId==userId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.VehicleBrand);
            AddInclude(x => x.VehicleCategory);
            AddInclude(x => x.VehicleEngineSize);
            AddInclude(x => x.VehicleFuelType);
            AddInclude(x => x.VehicleGearType);
            AddInclude(x => x.VehicleModel);
            AddInclude(x => x.VehicleAnnouncePhotos);
            AddOrderByDscending(x => x.Created);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }

        public VehicleAnnounceByUserIdSpecification(int userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.VehicleBrand);
            AddInclude(x => x.VehicleCategory);
            AddInclude(x => x.VehicleEngineSize);
            AddInclude(x => x.VehicleFuelType);
            AddInclude(x => x.VehicleGearType);
            AddInclude(x => x.VehicleModel);
            AddInclude(x => x.VehicleAnnouncePhotos);
        }

        public VehicleAnnounceByUserIdSpecification(int userId, int announceId) : base(x => x.UserId == userId && x.Id == announceId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.VehicleBrand);
            AddInclude(x => x.VehicleCategory);
            AddInclude(x => x.VehicleEngineSize);
            AddInclude(x => x.VehicleFuelType);
            AddInclude(x => x.VehicleGearType);
            AddInclude(x => x.VehicleModel);
            AddInclude(x => x.VehicleAnnouncePhotos);
        }
    }
}