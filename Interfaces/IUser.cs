using System.Collections.Generic;
using Netflix.Models;

namespace Netflix.App
{
    public interface IUser{
        List<UserHistoryEntry> GetUserHistory();
        void UpdateHistory(string id,float rank);

        void AddToHistory(string id,string title, string imdb_rating);

        bool HasWatched(string watchable_id);
    }
}
