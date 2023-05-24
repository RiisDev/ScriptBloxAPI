using System;
using ScriptBloxAPI.Backend_Functions;

namespace ScriptBloxAPI.DataTypes
{
    public class UserObject
    {
        /// <summary>
        /// Gets the count of scripts.
        /// </summary>
        public int ScriptsCount { get; }

        /// <summary>
        /// Gets the count of followers.
        /// </summary>
        public int FollowersCount { get; }

        /// <summary>
        /// Gets the count of following.
        /// </summary>
        public int FollowingCount { get; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Gets the biography.
        /// </summary>
        public string Bio { get; }

        /// <summary>
        /// Gets the avatar URL.
        /// </summary>
        public string Avatar { get; }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets a value indicating whether the user is verified.
        /// </summary>
        public bool Verified { get; }

        /// <summary>
        /// Gets the creation date and time.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the last active date and time.
        /// </summary>
        public DateTime LastActive { get; }


        public UserObject(int scriptsCount, int followersCount, int followingCount, string username, string bio, string avatar, string id, string createdAt, string lastActive, bool verified)
        {
            ScriptsCount = scriptsCount;
            FollowersCount = followersCount;
            FollowingCount = followingCount;
            Username = username;
            Bio = bio;
            Avatar = "https://scriptblox.com" + avatar;
            Id = id;
            Verified = verified;
            CreatedAt = MiscFunctions.ConvertStringToDateTime(createdAt);
            LastActive = MiscFunctions.ConvertStringToDateTime(lastActive);
        }
    }
}
