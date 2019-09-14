using System.Collections.Generic;
using Netflix.Models;

namespace Netflix.App
{
    public abstract class AbstractUserRepository : IUserRepository
    {
         public IUser GetAddUser(string user_id)
         {
                BeforeGetUser(user_id);
                IUser user = GetUser(user_id);
                AfterGetUser(user_id);
                return user;
         }

         protected abstract IUser GetUser(string user_id);
         protected virtual void BeforeGetUser(string user_id){}
         protected virtual void AfterGetUser(string user_id){}

        public List<UserHistoryEntry> GetUserHistory(string user_id)
        {
            IUser user = GetAddUser(user_id);
            return user.GetUserHistory();
        }

        public virtual void UpdateUserHistory(string user_id, string id, float rank)
        {
             IUser user = GetAddUser(user_id);
             user.UpdateHistory(id,rank);
        }

        public virtual void AddToUserHistory(string user_id, string id, string title, string imdb_rating)
        {
            IUser user = GetAddUser(user_id);
            user.AddToHistory(id,title,imdb_rating);
        }

        public bool UserHasWatched(string user_id, string watchable_id)
        {
            IUser user = GetAddUser(user_id);
            return user.HasWatched(watchable_id);
        }

        public virtual void Disconnect(){}
    }
}