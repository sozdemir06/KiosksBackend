using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IPublicfooterTextDal:IEntityRepository<PublicFooterText>
    {
         Task<PublicFooterText> GetFooterTextAsync();
    }
}