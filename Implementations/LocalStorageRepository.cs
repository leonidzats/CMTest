using Hanssens.Net;

namespace Netflix.App
{
    public class LocalStorageRepository : AbstractUserRepository,IUserRepository
    {
        private LocalStorage storeProvider;

        public LocalStorageRepository(LocalStorage store):base()
        {
            this.storeProvider = store;
        }

        protected override void BeforeGetUser(string user_id)
        {
            if (!this.storeProvider.Exists(user_id)){
                PersistentUser user = new PersistentUser(new User());
                this.storeProvider.Store(user_id,user);
            }
        }

        protected override IUser GetUser(string user_id)
        {
            PersistentUser user = this.storeProvider.Get<PersistentUser>(user_id);
            return new User(user);
        }

        public override void UpdateUserHistory(string user_id, string id, float rank){
            IUser user = getAddUser(user_id);
            user.UpdateHistory(id,rank);
            this.storeProvider.Store(user_id,new PersistentUser((User)user));
        }

        public override void AddToUserHistory(string user_id, string id, string title, string imdb_rating){
            IUser user = getAddUser(user_id);
            user.AddToHistory(id,title,imdb_rating);
            this.storeProvider.Store(user_id,new PersistentUser((User)user));
        }

        public override void Disconnect(){
            this.storeProvider.Persist();
        }
    }
}