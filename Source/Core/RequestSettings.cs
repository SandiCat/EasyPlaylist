namespace Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Google.GData.YouTube;
    using Google.YouTube;

    public class RequestSettings
    {
        const string _devKey = "";

        YouTubeRequestSettings _settings;

        public RequestSettings(string username, string password)
        {
            _settings = new YouTubeRequestSettings("EasyPlaylist", _devKey, username, password);
        }

        public YouTubeRequest MakeRequest()
        {
            return new YouTubeRequest(_settings);
        }
    }
}
