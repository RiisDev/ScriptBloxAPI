namespace ScriptBloxAPI.DataTypes
{
    public class CommentObject
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the count of likes.
        /// </summary>
        public int LikesCount { get; }

        /// <summary>
        /// Gets the count of dislikes.
        /// </summary>
        public int DislikesCount { get; }

        /// <summary>
        /// Gets the commenter.
        /// </summary>
        public UserObject Commenter { get; }


        public CommentObject(string id, string text, int likesCount, int dislikesCount, UserObject commenter)
        {
            Id = id;
            Text = text;
            LikesCount = likesCount;
            DislikesCount = dislikesCount;
            Commenter = commenter;
        }
    }
}
