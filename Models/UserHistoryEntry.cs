
using System;

namespace Netflix.Models
{
    public class UserHistoryEntry
    {
        public string Id{get;private set;}
        public string Title{get;private set;}
        public float UserRank{get;private set;}
        public string ImdbRank{get;private set;}
        public DateTime EntryDate {get;private set;}

        public void UpdateUserRank(float new_rank){
            this.UserRank = new_rank;
        }

        public UserHistoryEntry(string id,string title,string ImdbRank)
        {
            this.Id = id;
            this.Title = title;
            this.ImdbRank = ImdbRank;
            this.EntryDate = DateTime.Now;
        }

        public override string ToString(){
            return string.Format("watched at - {0} , title - {1} , user rank - {2} , imdb rank - {3}",
                            this.EntryDate.ToString("dddd, dd MMMM yyyy HH:mm"),
                            this.Title,
                            this.UserRank,
                            string.IsNullOrEmpty(this.ImdbRank)? 
                                            "unranked" : 
                                                this.ImdbRank);
        }

    }
    
}

