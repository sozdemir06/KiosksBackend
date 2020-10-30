using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.USerNotifyGroupsSpecification
{
    public class UserNotifyGroupWithByNotifyGroupId:BaseSpecification<UserNotifyGroup>
    {
        public UserNotifyGroupWithByNotifyGroupId(int notifyGroupId):base(x=>x.NotifyGroupId==notifyGroupId)
        {
            AddInclude(x=>x.NotifyGroup);
        }
    }
}