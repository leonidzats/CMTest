namespace Netflix.Models
{
    public class Watchable
    {
        public string id;
        public string title;
        public string overview;

        public System.DateTime released_on;

        public string imdb_rating;
        
        public enum WhatchableKind {Movie,Show,Both};

        public string Watchable_Id {get {return id;}}

        public override string ToString()
        {
            var nl = "\r\n";
            var date = this.released_on.ToShortDateString();
            return $"{nl}  title : {this.title}{nl}  overview : {this.overview}{nl}  released on : {date}{nl}  with imdb rating : {this.imdb_rating}";
        }

    }    
}
