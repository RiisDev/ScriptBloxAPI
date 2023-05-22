using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.DataTypes
{
    public class CommentObject
    {
        private string _id = string.Empty;
        private string _text = string.Empty;

        private int _likesCount = 0;
        private int _dislikesCount = 0;

        private UserObject _commenter;

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text => _text;

        /// <summary>
        /// Gets the count of likes.
        /// </summary>
        public int LikesCount => _likesCount;

        /// <summary>
        /// Gets the count of dislikes.
        /// </summary>
        public int DislikesCount => _dislikesCount;

        /// <summary>
        /// Gets the commenter.
        /// </summary>
        public UserObject Commenter => _commenter;



        public CommentObject(string id, string text, int likesCount, int dislikesCount, UserObject commenter)
        {
            _id = id;
            _text = text;
            _likesCount = likesCount;
            _dislikesCount = dislikesCount;
            _commenter = commenter;
        }
    }
}
