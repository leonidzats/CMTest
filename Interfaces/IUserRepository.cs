using System.Collections.Generic;
using Netflix.Models;

namespace Netflix.App
{
    public interface IUserRepository{
        IUser getAddUser(string user_id);

        List<UserHistoryEntry> GetUserHistory(string user_id);
        void UpdateUserHistory(string user_id,string id,float rank);

        void AddToUserHistory(string user_id,string id,string title, string imdb_rating);

        bool UserHasWatched(string user_id,string watchable_id);
    }
}