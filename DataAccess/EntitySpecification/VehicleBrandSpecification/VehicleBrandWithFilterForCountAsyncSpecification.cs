using Core.DataAccess.Specifications;
using Core.QueryParams;
using Entities.Concrete;

namespace DataAccess.EntitySpecification.VehicleBrandSpecification
{
    public class VehicleBrandWithFilterForCountAsyncSpecification : BaseSpecification<VehicleBrand>
    {

        public VehicleBrandWithFilterForCountAsyncSpecification(VehicleBrandParams queryParams)
        : base(x =>
            (string.IsNullOrEmpty(queryParams.Search)
                    || x.BrandName.ToLower().Contains(queryParams.Search)

                    ) &&
            (!queryParams.VehicleCategoryId.HasValue || x.VehicleCategoryId == queryParams.VehicleCategoryId)

         )
        {


        }
    }
}