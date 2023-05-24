namespace ScriptBloxAPI.DataTypes
{
    public  class GameObject
    {
        /// <summary>
        /// Gets the place ID.
        /// </summary>
        public long PlaceId { get; }

        /// <summary>
        /// Gets the game name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the game thumbnail URL.
        /// </summary>
        public string Thumbnail { get; }

        /// <summary>
        /// Gets the game URL.
        /// </summary>
        public string Url { get; }


        public GameObject(long gameId, string gameName, string gameThumbnail)
        {
            PlaceId = gameId;
            Name = gameName;
            Thumbnail = "https://scriptblox.com"+gameThumbnail;
            Url = $"https://www.roblox.com/games/{gameId}";
        }
    }
}
