using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Security.UserAccessor;

namespace API.Hubs
{
    public class UserTracker
    {
        private readonly IUserAccessor userAccessor;
        public UserTracker(IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;

        }
        private static readonly Dictionary<int, List<string>> OnlineUsers =
      new Dictionary<int, List<string>>();

        public Task UserConnected(int userId, string connectionId)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(userId))
                {
                    OnlineUsers[userId].Add(connectionId);
                }
                else
                {
                    OnlineUsers.Add(userId, new List<string> { connectionId });
                }
            }

            return Task.CompletedTask;
        }

        public Task UserDisConnected(int userId, string connectionId)
        {
            lock (OnlineUsers)
            {
                if (!OnlineUsers.ContainsKey(userId)) return Task.CompletedTask;

                OnlineUsers[userId].Remove(connectionId);
                if (OnlineUsers[userId].Count == 0)
                {
                    OnlineUsers.Remove(userId);
                }
            }

            return Task.CompletedTask;
        }

        public Task<int[]> GetOnlineUsers()
        {
            int[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }

        public async Task<string[]> GetOnlineUser()
        {
            string[] onlineUser=null;
            var userId=await userAccessor.GetUserClaimId();
            lock (OnlineUsers)
            {
                var users = OnlineUsers.OrderBy(k => k.Key).Where(x=>x.Key==userId).ToList();
                foreach (var item in users)
                {
                    onlineUser=item.Value.ToArray();
                };
                
            }

            return await Task.FromResult(onlineUser);
        }


    }
}