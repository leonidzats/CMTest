using System.Collections.Generic;
using Netflix.App.Api;
using Netflix.Models;

namespace Netflix.App
{
        public class NetflixApp
        {   
            private readonly IUserRepository userRepository;
            private readonly INetflixApi netflixApi;

            public NetflixApp(IUserRepository user_repository,INetflixApi netflix_api)
            {
                this.userRepository = user_repository;
                this.netflixApi = netflix_api;
            }
            public bool login(string username){
                this.userRepository.getAddUser(username);
                return true;
            }

            public void Close(){
                this.userRepository.Disconnect();
            }
            public List<UserHistoryEntry> GetUserHistory(string username){
                return this.userRepository.GetUserHistory(username);
            }
            
            public Watchable WatchSomething(string username,string content_choice){
                var watchable = this.netflixApi.GetSomethingToWatch(GetWatchableKindFromChoice(content_choice));
                
                while (this.userRepository.UserHasWatched(username,watchable.Watchable_Id)){
                    watchable = this.netflixApi.GetSomethingToWatch(GetWatchableKindFromChoice(content_choice));
                }
                this.userRepository.AddToUserHistory(username,watchable.id,watchable.title,watchable.imdb_rating);
                return watchable;
            }

            public void UpdateContentRank(string username,Watchable content,float rank){
                this.userRepository.UpdateUserHistory(username,content.id,rank);
            }
            private Watchable.WhatchableKind GetWatchableKindFromChoice(string choice){
                switch(choice){
                    case "1" : return Watchable.WhatchableKind.Movie;
                    case "2" : return Watchable.WhatchableKind.Show;
                    case "3" : return Watchable.WhatchableKind.Both;
                    default : return Watchable.WhatchableKind.Both;
                }
            }
    }
}
