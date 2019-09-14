clone the repo:
    
    git clone https://github.com/leonidzats/CMTest.git

To execute regular in memory:

    dotnet run
    
To execute local persistence:
  
    dotnet run --persistToLocal


# CMTest
Required flow (& features):

    Each user will enter their user ID. 
    They’ll then choose whether they want to see a content, history or exit (C/H/E/S)
    If they choose S – switch user, ask for user ID again.
    If they choose E – exit gracefully
    If they choose H – show them the user’s history (make sure the history shown is for the correct user):
    UserId
    Watched content, and for each content:
    Title, UserRanking, ImdbRanking (if exists) 
    If they choose Content:
    Ask them what kind of content (1 – TV Show, 2 – Movie, 3 – Any)
    Randomly choose something to show to the user:
    Make sure that we don’t offer the same content twice for the same user.
    Once the user is watching a movie, ask him whether he finished watching (Y/N)
    If the answer is ‘N’, sleep for 10 seconds before asking again.
    If the answer is ‘Y’:
    Ask the user after the recommendation to rank the content (0-10).

And again

API we’ll use:

    Requests - The API you’ll use to get a random series is:
    https://api.reelgood.com/v1/roulette/netflix?nocache=true&content_kind=both&availability=onAnySource
    We want to support different content_kind requests for TV shows, Movies or Both:
    content_kind possible values are: {both, movie, show}

Each request will give a new random show/movie to watch.

Response – is a JSON object, for example:

```json
    {
       "id":"29fbd74a-f25a-40eb-9393-449e35c9f118",
       "slug":"hirschen-da-machst-was-mit-2015",
       "title":"Hirschen - Da machst was mit!",
       "overview":"The residents of Hirschen stumble on a creative idea to boost their struggling village's economy after a driver runs into a deer and seeks their help.",
       "tagline":"",
       "classification":null,
       "runtime":90,
       "released_on":"2015-06-04T00:00:00",
       "has_poster":true,
       "has_backdrop":true,
       "imdb_rating":5.2,
       "rt_critics_rating":null,
       "genres":[9],
       "watchlisted":false,
       "seen":false,
       "content_type":"m"
    }
    ```
We want to save and show the fields marked in yellow once a user has requested a random Netflix movie/show.
