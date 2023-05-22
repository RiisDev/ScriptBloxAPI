namespace ScriptBloxAPI.DataTypes
{
    public  class GameObject
    {
        private int _gameId = 0;
        private string _gameName = string.Empty;   
        private string _gameThumbnail = string.Empty;
        private string _gameUrl = string.Empty;

        /// <summary>
        /// Gets the place ID.
        /// </summary>
        public int PlaceId { get { return _gameId; } }

        /// <summary>
        /// Gets the game name.
        /// </summary>
        public string Name { get { return _gameName; } }

        /// <summary>
        /// Gets the game thumbnail URL.
        /// </summary>
        public string Thumbnail { get { return _gameThumbnail; } }

        /// <summary>
        /// Gets the game URL.
        /// </summary>
        public string Url { get { return _gameUrl; } }


        public GameObject(int gameId, string gameName, string gameThumbnail)
        {
            _gameId = gameId;
            _gameName = gameName;
            _gameThumbnail = "https://scriptblox.com"+gameThumbnail;
            _gameUrl = $"https://www.roblox.com/games/{gameId}";
        }
    }
}
