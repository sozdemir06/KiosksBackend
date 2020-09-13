using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLiveTvListDal:EfEntityRepositoryBase<LiveTvList,DataContext>,ILiveTvListDal
    {
        
    }
}