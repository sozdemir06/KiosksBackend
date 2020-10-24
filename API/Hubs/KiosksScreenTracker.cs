using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class KiosksScreenTracker
    {
        private static readonly Dictionary<int, List<string>> ConnectedScreens =
        new Dictionary<int, List<string>>();


        public Task ScreenConnected(int screenId, string connectionId)
        {
            lock (ConnectedScreens)
            {
                if (ConnectedScreens.ContainsKey(screenId))
                {
                    ConnectedScreens[screenId].Add(connectionId);
                }
                else
                {
                    ConnectedScreens.Add(screenId, new List<string> { connectionId });
                }
            }

            return Task.CompletedTask;
        }

        public Task ScreenDisConnected(int screenId,string connectionId)
        {
            lock(ConnectedScreens)
            {
                if(!ConnectedScreens.ContainsKey(screenId))return Task.CompletedTask;

                ConnectedScreens[screenId].Remove(connectionId);
                if(ConnectedScreens[screenId].Count==0)
                {
                    ConnectedScreens.Remove(screenId);
                }
            }

            return Task.CompletedTask;
        }

        public Task<int[]> GetConnectedScreen()
        {
            int[] onlineScreens;
            lock(ConnectedScreens)
            {
                onlineScreens=ConnectedScreens.OrderBy(k=>k.Key).Select(k=>k.Key).ToArray();
            }

            return Task.FromResult(onlineScreens);
        }

         public async Task<string[]> GetOnlineScreens()
        {
            string[] onlinescreens=null;
            lock (ConnectedScreens)
            {
                var users = ConnectedScreens.OrderBy(k => k.Key).ToList();
                foreach (var item in users)
                {
                    onlinescreens=item.Value.ToArray();
                };
                
            }

            return await Task.FromResult(onlinescreens);
        }
    }
}