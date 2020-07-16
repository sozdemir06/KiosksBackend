using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;


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