using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVehicleCategoryDal:EfEntityRepositoryBase<VehicleCategory,DataContext>,IVehicleCategoryDal
    {
        
    }
}