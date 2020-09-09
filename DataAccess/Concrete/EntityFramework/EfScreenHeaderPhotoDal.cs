using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfScreenHeaderPhotoDal:EfEntityRepositoryBase<ScreenHeaderPhoto,DataContext>,IScreenHeaderPhotoDal
    {
        
    }
}