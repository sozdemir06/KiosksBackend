using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPublicFooterTextDal : EfEntityRepositoryBase<PublicFooterText, DataContext>, IPublicfooterTextDal
    {
        public async Task<PublicFooterText> GetFooterTextAsync()
        {
            using(var context=new DataContext())
            {
                return await context.PublicFooterTexts.FirstOrDefaultAsync();
            }
        }
    }
}