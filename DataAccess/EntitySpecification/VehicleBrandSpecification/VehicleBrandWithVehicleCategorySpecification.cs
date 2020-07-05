using Core.DataAccess.Specifications;
using Core.QueryParams;
using Entities.Concrete;

namespace DataAccess.EntitySpecification.VehicleBrandSpecification
{
    public class VehicleBrandWithVehicleCategorySpecification : BaseSpecification<VehicleBrand>
    {
        public VehicleBrandWithVehicleCategorySpecification(VehicleBrandParams queryParams)
        : base(x =>
             (
                 string.IsNullOrEmpty(queryParams.Search) || 
                 x.BrandName.ToLower().Contains(queryParams.Search) 
            ) &&
            (!queryParams.VehicleCategoryId.HasValue || x.VehicleCategoryId==queryParams.VehicleCategoryId)

         )
        {
            AddInclude(x=>x.VehicleCategories);
            AddOrderBy(x=>x.BrandName);
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
        }
    }
}