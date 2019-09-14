
using Netflix.Models;

namespace Netflix.App.Api
{
    public interface INetflixApi{
        Watchable GetSomethingToWatch(Watchable.WhatchableKind kind);
    }
}