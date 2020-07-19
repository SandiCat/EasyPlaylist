using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;

namespace Core
{
    public class PlaylistMaker
    {
        string[] _queryList;
        string _playlistName;
        string _playlistSummary;
        YouTubeRequest _request;

        public string PlaylistUrl;


        public PlaylistMaker(string queryList, string playlistName, string playlistSummary, RequestSettings settings)
        {
            _queryList = queryList.Split('\n');
            _playlistName = playlistName;
            _playlistSummary = playlistSummary;
            _request = settings.MakeRequest();
        }

        public void CreatePlaylist()
        {
            Playlist playlistBlueprint = new Playlist();
            playlistBlueprint.Title = _playlistName;
            playlistBlueprint.Summary = _playlistSummary;

            Playlist playlist;
            try
            {
                playlist = _request.Insert(new Uri("http://gdata.youtube.com/feeds/api/users/default/playlists"), playlistBlueprint);
            }
            catch (InvalidCredentialsException)
            {
                throw new InvalidLoginInfoException();
            }

            foreach (var query in _queryList)
            {
                YouTubeQuery ytQuery = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
                //ytQuery.OrderBy = "viewCount";
                ytQuery.Query = query;
                ytQuery.SafeSearch = YouTubeQuery.SafeSearchValues.None;

                Feed<Video> results = _request.Get<Video>(ytQuery);
                Video firstVideo = results.Entries.ToList()[0];

                PlayListMember playlistMember = new PlayListMember();
                playlistMember.VideoId = firstVideo.VideoId;
                _request.AddToPlaylist(playlist, playlistMember);
            }

            int idIndex = playlist.Id.IndexOf("playlist:");
            string id = playlist.Id.Substring(idIndex + "playlist:".Length);
            PlaylistUrl = "https://www.youtube.com/playlist?list=" + id;
        }
    }
}
