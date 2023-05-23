using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.DataTypes
{
    public class NotificationObject
    {
        private string _id = string.Empty;
        private DateTime _createdAt = DateTime.MinValue;
        private NotificationType _notificationType = NotificationType.CommentLiked;
        private bool isRead = false;

        private UserObject userSender = null;
        private ScriptObject scriptSender = null;

        public string Id => _id;
        public DateTime CreatedAt => _createdAt;
        public NotificationType Type => _notificationType;
        public bool IsRead => isRead;
        public ScriptObject ScriptObjectSender => scriptSender;
        public UserObject UserObjectSender => userSender;

        public enum NotificationType
        {
            CommentLiked,
            CommentDisliked,
            ScriptLiked,
            ScriptDisliked,
            CommentAddedToScript,
            Followed,
        }

        public NotificationObject(string id, DateTime createdAt, NotificationType notificationType, bool isRead, UserObject userSender = null, ScriptObject scriptSender = null)
        {
            if (userSender == null && scriptSender == null) throw new ScriptBloxException("Must provide either a UserObject or ScriptObject");

            _id = id;
            _createdAt = createdAt;
            _notificationType = notificationType;
            this.isRead = isRead;
            this.userSender = userSender;
            this.scriptSender = scriptSender;
        }
    }
}
