using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;


namespace DataAccess.EntitySpecification.VehicleModelSpecification
{
    public class VehicleModelWithFilterCountAsync : BaseSpecification<VehicleModel>
    {
        public VehicleModelWithFilterCountAsync(VehicleModelParams queryParams)
        : base(x =>
             (string.IsNullOrEmpty(queryParams.Search) || x.VehicleModelName.ToLower().Contains(queryParams.Search)) &&
             (!queryParams.VehicleBrandId.HasValue || x.VehicleBrandId == queryParams.VehicleBrandId) &&
             (!queryParams.VehicleCategoryId.HasValue || x.VehicleCategoryId == queryParams.VehicleCategoryId)
        )
        {

        }
    }
}