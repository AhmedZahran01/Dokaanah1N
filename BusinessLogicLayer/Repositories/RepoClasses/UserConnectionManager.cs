//using Dokaanah.BusinessLogicLayer.Repositories.RepoInterfaces;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dokaanah.BusinessLogicLayer.Repositories.RepoClasses
//{  
//    public class UserConnectionManager : IUserConnectionManager
//    {
//        private readonly ConcurrentDictionary<string, string> _onlineUsers = new();

//        public void AddConnection(string userId, string connectionId)
//        {
//            _onlineUsers[userId] = connectionId;
//        }

//        public void RemoveConnection(string connectionId)
//        {
//            var userId = _onlineUsers.FirstOrDefault(x => x.Value == connectionId).Key;
//            if (!string.IsNullOrEmpty(userId))
//            {
//                _onlineUsers.TryRemove(userId, out _);
//            }

//        }

//        public IEnumerable<string> GetOnlineUsers() => _onlineUsers.Keys;
//    }



//}
