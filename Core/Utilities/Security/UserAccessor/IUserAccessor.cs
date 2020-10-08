using System.Threading.Tasks;

namespace Core.Utilities.Security.UserAccessor
{
    public interface IUserAccessor
    {
         Task<int> GetUserClaimId();
    }
}