using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.DataTypes
{
    public class UserObject
    {
        private int _scriptsCount = 0;
        private int _followersCount = 0;
        private int _followingCount = 0;

        private string _username = string.Empty;
        private string _bio = string.Empty;
        private string _avatar = string.Empty;
        private string _id = string.Empty;

        private bool _verified = false;
        
        private DateTime _createdAt = DateTime.MinValue;
        private DateTime _lastActive = DateTime.MinValue;

        /// <summary>
        /// Gets the count of scripts.
        /// </summary>
        public int ScriptsCount => _scriptsCount;

        /// <summary>
        /// Gets the count of followers.
        /// </summary>
        public int FollowersCount => _followersCount;

        /// <summary>
        /// Gets the count of following.
        /// </summary>
        public int FollowingCount => _followingCount;

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username => _username;

        /// <summary>
        /// Gets the biography.
        /// </summary>
        public string Bio => _bio;

        /// <summary>
        /// Gets the avatar URL.
        /// </summary>
        public string Avatar => _avatar;

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Gets a value indicating whether the user is verified.
        /// </summary>
        public bool Verified => _verified;

        /// <summary>
        /// Gets the creation date and time.
        /// </summary>
        public DateTime CreatedAt => _createdAt;

        /// <summary>
        /// Gets the last active date and time.
        /// </summary>
        public DateTime LastActive => _lastActive;


        public UserObject(int scriptsCount, int followersCount, int followingCount, string username, string bio, string avatar, string id, string createdAt, string lastActive, bool verified)
        {
            _scriptsCount = scriptsCount;
            _followersCount = followersCount;
            _followingCount = followingCount;
            _username = username;
            _bio = bio;
            _avatar = "https://scriptblox.com" + avatar;
            _id = id;
            _verified = verified;
            _createdAt = MiscFunctions.ConvertStringToDateTime(createdAt);
            _lastActive = MiscFunctions.ConvertStringToDateTime(lastActive);
        }
    }
}
