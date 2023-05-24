using System;
using ScriptBloxAPI.Backend_Functions;

namespace ScriptBloxAPI.DataTypes
{
    public class NotificationObject
    {
        public string Id { get; }

        public DateTime CreatedAt { get; }

        public NotificationType Type { get; }

        public bool IsRead { get; }

        public ScriptObject ScriptObjectSender { get; }

        public UserObject UserObjectSender { get; }

        public enum NotificationType
        {
            CommentLiked,
            CommentDisliked,
            ScriptLiked,
            ScriptDisliked,
            CommentAddedToScript,
            Followed
        }

        public NotificationObject(string id, DateTime createdAt, NotificationType notificationType, bool isRead, UserObject userSender = null, ScriptObject scriptSender = null)
        {
            if (userSender == null && scriptSender == null) throw new ScriptBloxException("Must provide either a UserObject or ScriptObject");

            Id = id;
            CreatedAt = createdAt;
            Type = notificationType;
            IsRead = isRead;
            UserObjectSender = userSender;
            ScriptObjectSender = scriptSender;
        }
    }
}
