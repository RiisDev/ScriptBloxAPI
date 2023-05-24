﻿using Newtonsoft.Json.Linq;
using ScriptBloxAPI.DataTypes;
using System.Collections.Generic;
using static ScriptBloxAPI.DataTypes.NotificationObject;

namespace ScriptBloxAPI.Methods
{
    public class Notifications
    {
        internal static Dictionary<string, NotificationType> _actionToType = new Dictionary<string, NotificationType>()
        {
            { "followed", NotificationType.Followed },
            { "commented", NotificationType.CommentAddedToScript },

            { "SCRIPT_disliked", NotificationType.ScriptDisliked },
            { "SCRIPT_liked", NotificationType.ScriptLiked },
            { "COMM_disliked", NotificationType.CommentDisliked},
            { "COMM_liked", NotificationType.CommentLiked },
        };

        /// <summary>
        /// Retrieves all notifications for a user with the specified authorization.
        /// </summary>
        /// <param name="authorization">The authorization token for the user.</param>
        /// <returns>A list of NotificationObject containing the retrieved notifications.</returns>
        public static List<NotificationObject> GetAllNotifications(string authorization)
        {
            MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);
            string jsonReturnString = MiscFunctions.HttpClient.GetStringAsync("https://scriptblox.com/api/user/notifications").Result;
            MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            if (string.IsNullOrEmpty(jsonReturnString))
                throw new ScriptBloxException("An error has occurred while fetching the JSON, please submit a bug report.");

            JToken jsonReturn = JToken.Parse(jsonReturnString);
            JArray jsonNotifications = (JArray)jsonReturn["notifications"];

            if (jsonNotifications == null)
                throw new ScriptBloxException("Backend error occurred.");

            List<NotificationObject> notifications = new List<NotificationObject>(jsonNotifications.Count);

            foreach (JToken jsonNotification in jsonNotifications)
            {
                NotificationObject notification = ParseNotification(jsonNotification);
                notifications.Add(notification);
            }

            return notifications;
        }

        /// <summary>
        /// Retrieves a list of notifications of a specific type based on the provided authorization and notification type.
        /// </summary>
        /// <param name="authorization">The authorization token or key required to access the notifications.</param>
        /// <param name="type">The desired type of notifications to retrieve.</param>
        /// <returns>A list of <see cref="NotificationObject"/> objects that match the specified type.</returns>
        public static List<NotificationObject> GetNotificationsByType(string authorization, NotificationType type)
        {
            List<NotificationObject> notifications = GetAllNotifications(authorization);
            List<NotificationObject> parsedNotifications = new List<NotificationObject>();

            foreach (NotificationObject notification in notifications)
                if (notification.Type == type)
                    parsedNotifications.Add(notification);

            return parsedNotifications;
        }

        #region Internal Functions
        private static NotificationObject ParseNotification(JToken jsonNotification)
        {
            string reference = jsonNotification.Value<string>("reference");
            string action = jsonNotification.Value<string>("action");
            string slug = "";

            UserObject userObject = null;
            ScriptObject scriptObject = null;
            NotificationType notificationType = NotificationType.CommentDisliked;

            if (reference == "User")
            {
                userObject = UserMethods.GetUserFromUsername(jsonNotification["target"]["username"].Value<string>());
                notificationType = _actionToType[action];
            }
            else if (reference == "Script")
            {
                string actionType = jsonNotification.Value<string>("type");

                if (jsonNotification["target"]["slug"] != null)
                    slug = jsonNotification["target"]["slug"].Value<string>();

                if (action == "commented" || (action == "disliked" || action == "liked" && actionType == "Script"))
                {
                    scriptObject = ScriptsMethods.GetScriptFromScriptbloxId(slug);
                    notificationType = _actionToType[action];
                }
                else if (action == "disliked" || action == "liked")
                {
                    scriptObject = ScriptsMethods.GetScriptFromScriptbloxId(slug);
                    string scriptAction = "COMM__" + action;
                    notificationType = _actionToType[scriptAction];
                }
            }

            return new NotificationObject(
                jsonNotification.Value<string>("_id"),
                MiscFunctions.ConvertStringToDateTime(jsonNotification.Value<string>("createdAt")),
                notificationType,
                jsonNotification.Value<bool>("isRead"),
                userObject,
                scriptObject
            );
        }
        #endregion
    }
}