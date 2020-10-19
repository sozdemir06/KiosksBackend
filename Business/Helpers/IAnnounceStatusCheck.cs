using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Helpers
{
    public interface IAnnounceStatusCheck
    {
         Task<bool> CheckAnnounceStatus(AnnounceForReturnDto announce);
    }
}