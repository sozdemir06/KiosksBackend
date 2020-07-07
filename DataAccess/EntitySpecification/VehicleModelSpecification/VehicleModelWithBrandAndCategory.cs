using Core.DataAccess.Specifications;
using Core.QueryParams;
using Entities.Concrete;

namespace DataAccess.EntitySpecification.VehicleModelSpecification
{
    public class VehicleModelWithBrandAndCategory:BaseSpecification<VehicleModel>
    {
        public VehicleModelWithBrandAndCategory(VehicleModelParams queryParams)
        :base(x=>
            (
              string.IsNullOrEmpty(queryParams.Search) || 
              x.VehicleModelName.ToLower().Contains(queryParams.Search) ||
              x.VehicleBrands.BrandName.ToLower().Contains(queryParams.Search)
              )&&
            (!queryParams.VehicleBrandId.HasValue || x.VehicleBrandId==queryParams.VehicleBrandId) &&
            (!queryParams.VehicleCategoryId.HasValue || x.VehicleCategoryId==queryParams.VehicleCategoryId)
        )
        {
           AddInclude(x=>x.VehicleBrands);
           AddInclude(x=>x.VehicleCategories);
           AddOrderBy(x=>x.VehicleModelName);
           ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
        }
    }
}