using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IKiosksService
    {
         Task<KiosksForReturnDto> KiosksAsync(int screenId);
    }
}