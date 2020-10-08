using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.USerNotifyGroupsSpecification
{
    public class UserNotifyGroupWithNotifyGroupSpecification:BaseSpecification<UserNotifyGroup>
    {
        public UserNotifyGroupWithNotifyGroupSpecification()
        {
            AddInclude(x=>x.NotifyGroup);
        }

        public UserNotifyGroupWithNotifyGroupSpecification(int userId):base(x=>x.UserId==userId)
        {
            AddInclude(x=>x.NotifyGroup);
        }
    }
}