using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI
{
    public class Script
    {
        private GameObject game;

        private string _scriptTitle = string.Empty;
        private string _scriptId = string.Empty;
        private string _scriptBloxId = string.Empty;
        private string _executingScript = string.Empty;

        private int _scriptViews = 0;
        private int _scriptLikes = 0;
        private int _scriptDislikes = 0;

        private bool _isUniversal = false;
        private bool _isPatched = false;
        private bool _requiresKey = false;
        private bool _isVerified = false;

        private List<string> _scriptTags = new List<string>();

        private DateTime _createdAt = DateTime.MinValue;
        private DateTime _updatedAt = DateTime.MinValue;

        /// <summary>
        /// Gets the game object associated with the script.
        /// </summary>
        public GameObject Game => game;

        /// <summary>
        /// Gets the list of tags associated with the script.
        /// </summary>
        public List<string> Tags => _scriptTags;

        /// <summary>
        /// Gets the title of the script.
        /// </summary>
        public string Title => _scriptTitle;

        /// <summary>
        /// Gets the ID of the script.
        /// </summary>
        public string Id => _scriptId;

        /// <summary>
        /// Gets the ScriptBlox ID of the script.
        /// </summary>
        public string ScriptBloxId => _scriptBloxId;

        /// <summary>
        /// Gets the executing script name.
        /// </summary>
        public string ExecutingScript => _executingScript;

        /// <summary>
        /// Gets the number of views on the script.
        /// </summary>
        public int Views => _scriptViews;

        /// <summary>
        /// Gets the number of likes on the script.
        /// </summary>
        public int Likes => _scriptLikes;

        /// <summary>
        /// Gets the number of dislikes on the script.
        /// </summary>
        public int Dislikes => _scriptDislikes;

        /// <summary>
        /// Gets a value indicating whether the script is universal.
        /// </summary>
        public bool IsUniversal => _isUniversal;

        /// <summary>
        /// Gets a value indicating whether the script is patched.
        /// </summary>
        public bool IsPatched => _isPatched;

        /// <summary>
        /// Gets a value indicating whether the script is verified.
        /// </summary>
        public bool IsVerified => _isVerified;

        /// <summary>
        /// Gets a value indicating whether the script requires a key.
        /// </summary>
        public bool RequiresKey => _requiresKey;

        /// <summary>
        /// Gets the creation date and time of the script.
        /// </summary>
        public DateTime CreatedAt => _createdAt;

        /// <summary>
        /// Gets the last updated date and time of the script.
        /// </summary>
        public DateTime UpdatedAt => _updatedAt;


        public Script(GameObject game, string scriptTitle, string scriptId, string scriptBloxId, string ExecutingScript, string epocUpdated, string epocCreated, int scriptViews, int scriptLikes, int scriptDislikes, bool isUniversal, bool isPatched, bool requireskey, bool isverified, List<string> scriptTags)
        {
            this.game = game;
            _scriptTitle = scriptTitle;
            _scriptId = scriptId;
            _scriptBloxId = scriptBloxId;
            _scriptViews = scriptViews;
            _scriptLikes = scriptLikes;
            _scriptDislikes = scriptDislikes;
            _isUniversal = isUniversal;
            _isPatched = isPatched;
            _scriptTags = scriptTags;
            _executingScript = ExecutingScript;
            _isVerified = isverified;
            _requiresKey = requireskey;

            _updatedAt = MiscFunctions.ConvertStringToDateTime(epocUpdated);
            _createdAt = MiscFunctions.ConvertStringToDateTime(epocCreated);
        }
    }
}
