using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Netflix.Models;

namespace Netflix.App
{
    internal class PersistentUser {
        public PersistentUser(User user)
        {
            this.historyEntries = user?.GetUserHistory() ?? new List<UserHistoryEntry>();
        }

        public List<UserHistoryEntry> HistoryEntries { get {return this.historyEntries;}set{this.historyEntries = value;} }
        private List<UserHistoryEntry> historyEntries;
    }
    
    internal class User : IUser
    {
        private ConcurrentDictionary<string,UserHistoryEntry> history;

        public User(PersistentUser persistentUser){
            var history_list = persistentUser.HistoryEntries.ToDictionary(x=>x.Id,x=>x);
            this.history = new ConcurrentDictionary<string, UserHistoryEntry>(history_list);
        }
        public User()
        {
            this.history = new ConcurrentDictionary<string, UserHistoryEntry>();
        }

        public void AddToHistory(string id, string title, string imdb_rating)
        {
            this.history[id] = new UserHistoryEntry(id,title,imdb_rating);
        }

        public List<UserHistoryEntry> GetUserHistory()
        {
            return this.history.Values.ToArray().ToList();
        }

        public bool HasWatched(string watchable_id)
        {
            UserHistoryEntry e;
            this.history.TryGetValue(watchable_id,out e);
            if (e == null)
                return false;
            else
                return true;
        }

        public void UpdateHistory(string id, float rank)
        {
            this.history[id].UpdateUserRank(rank);
        }
    }

}
