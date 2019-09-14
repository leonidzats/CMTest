using Newtonsoft.Json;
using Netflix.Models;

namespace Netflix.App.Api
{
    public class NetflixApi : INetflixApi
    {
        private readonly string url_template = "https://api.reelgood.com/v1/roulette/netflix?nocache=true&amp;content_kind={0}&amp;availability=onAnySource";
        private string CallApi(string url)
        {
            using (var client = new System.Net.Http.HttpClient())
                {
                    return client.GetStringAsync(url).Result; //uri
                } 
        }

        private string GetUrlByKind(Watchable.WhatchableKind kind){
            string format_string = "both";
            switch(kind){
                    case Watchable.WhatchableKind.Movie: format_string = "movie";
                        break;
                    case Watchable.WhatchableKind.Show: format_string = "show";
                        break;
            }
            return string.Format(this.url_template,format_string);
        }

        public Watchable GetSomethingToWatch(Watchable.WhatchableKind kind)
        {
            string api_response = CallApi(GetUrlByKind(kind));
            Watchable watchable = JsonConvert.DeserializeObject<Watchable>(api_response);
            return watchable;
        }
    }
}
