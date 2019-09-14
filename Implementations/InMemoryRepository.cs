using System.Collections.Concurrent;
using System.Collections.Generic;
using Hanssens.Net;
using Netflix.Models;

namespace Netflix.App
{
    public class InMemoryRepository : AbstractUserRepository,IUserRepository
    {
        ConcurrentDictionary<string,IUser> users;
        
        public InMemoryRepository()
        {
            users = new ConcurrentDictionary<string, IUser>();
        }
        protected override IUser GetUser(string user_id)
        {
             try
            {
                IUser user;
                users.TryGetValue(user_id,out user);
                if (user == null){
                    users[user_id] = new User();
                    return users[user_id];
                }
                return user;    
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("an error occured logging in , please try again later",ex);
            }
        }
    }

}
