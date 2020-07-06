using Core.DataAccess.Specifications;
using Core.QueryParams;
using Entities.Concrete;

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