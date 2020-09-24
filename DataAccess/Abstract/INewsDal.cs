using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface INewsDal : IEntityRepository<News>
    {
        Task<List<News>> GetNewsForKiosksByScreenIdAsync(int screenId);
        Task<List<News>> GetNewsForKiosksBySubScreenIdAsync(int subScreenId);
        Task<List<News>> GetNewsForPublicDto();
    }
}