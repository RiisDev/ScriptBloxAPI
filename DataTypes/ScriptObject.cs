using System;
using System.Collections.Generic;
using ScriptBloxAPI.Backend_Functions;

namespace ScriptBloxAPI.DataTypes
{
    public class ScriptObject
    {
        /// <summary>
        /// Gets the game object associated with the script.
        /// </summary>
        public GameObject Game { get; }

        /// <summary>
        /// Gets the list of tags associated with the script.
        /// </summary>
        public List<string> Tags { get; }

        /// <summary>
        /// Gets the title of the script.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the ID of the script.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the ScriptBlox ID of the script.
        /// </summary>
        public string ScriptBloxId { get; }

        /// <summary>
        /// Gets the executing script name.
        /// </summary>
        public string ExecutingScript { get; }

        /// <summary>
        /// Gets the number of views on the script.
        /// </summary>
        public long Views { get; }

        /// <summary>
        /// Gets the number of likes on the script.
        /// </summary>
        public long Likes { get; }

        /// <summary>
        /// Gets the number of dislikes on the script.
        /// </summary>
        public long Dislikes { get; }

        /// <summary>
        /// Gets a value indicating whether the script is universal.
        /// </summary>
        public bool IsUniversal { get; }

        /// <summary>
        /// Gets a value indicating whether the script is patched.
        /// </summary>
        public bool IsPatched { get; }

        /// <summary>
        /// Gets a value indicating whether the script is verified.
        /// </summary>
        public bool IsVerified { get; }

        /// <summary>
        /// Gets a value indicating whether the script requires a key.
        /// </summary>
        public bool RequiresKey { get; }

        /// <summary>
        /// Gets the creation date and time of the script.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the last updated date and time of the script.
        /// </summary>
        public DateTime UpdatedAt { get; }


        public ScriptObject(GameObject game, string scriptTitle, string scriptId, string scriptBloxId, string executingScript, string epocUpdated, string epocCreated, long scriptViews, long scriptLikes, long scriptDislikes, bool isUniversal, bool isPatched, bool requiresKey, bool isVerified, List<string> scriptTags)
        {
            Game = game;
            Title = scriptTitle;
            Id = scriptId;
            ScriptBloxId = scriptBloxId;
            Views = scriptViews;
            Likes = scriptLikes;
            Dislikes = scriptDislikes;
            IsUniversal = isUniversal;
            IsPatched = isPatched;
            Tags = scriptTags;
            ExecutingScript = executingScript;
            IsVerified = isVerified;
            RequiresKey = requiresKey;

            UpdatedAt = MiscFunctions.ConvertStringToDateTime(epocUpdated);
            CreatedAt = MiscFunctions.ConvertStringToDateTime(epocCreated);
        }
    }
}
