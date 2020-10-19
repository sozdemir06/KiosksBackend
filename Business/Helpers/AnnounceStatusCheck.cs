using System;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Helpers
{
    public class AnnounceStatusCheck : IAnnounceStatusCheck
    {
        public Task<bool> CheckAnnounceStatus(AnnounceForReturnDto announce)
        {
             bool check=false;
             if((!announce.IsNew && announce.IsPublish==true && !announce.Reject &&
                announce.PublishStartDate <= DateTime.Now && announce.PublishFinishDate >= DateTime.Now) || (announce.PublishStartDate >= DateTime.Now))
            {
                check=true;
            }else{
                check=false;
            }

            return Task.FromResult(check);
        }
    }
}