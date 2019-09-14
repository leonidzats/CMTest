
using System;

namespace Netflix.Models
{
    public class UserHistoryEntry
    {
        public string Id{get;private set;}
        public string Title{get;private set;}
        public string UserRank{get;private set;}
        public string ImdbRank{get;private set;}
        public string EntryDate {get;private set;}

        public void UpdateUserRank(float new_rank){
            this.UserRank = new_rank.ToString(".0");
        }

        public UserHistoryEntry(string id,string title,string ImdbRank,float userRank=0,string EntryDate=null)
        {
            this.Id = id;
            this.Title = title;
            this.ImdbRank = ImdbRank;
            this.EntryDate = EntryDate ?? DateTime.Now.ToString();
            this.UserRank = userRank.ToString(".0");
        }

        public override string ToString(){
            return string.Format("watched at - {0} , title - {1} , user rank - {2} , imdb rank - {3}",
                            DateTime.Parse(this.EntryDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                            this.Title,
                            this.UserRank,
                            string.IsNullOrEmpty(this.ImdbRank)? 
                                            "unranked" : 
                                                this.ImdbRank);
        }

    }
    
}

