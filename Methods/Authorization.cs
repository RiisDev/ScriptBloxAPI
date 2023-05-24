using ScriptBloxAPI.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Methods
{
    public class Authorization
    {
        /// <summary>
        /// Sends a ping request to the idle endpoint.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <returns>A BoolStatus object indicating if the request was successful and the response status code.</returns>
        public static BoolStatus PingIdle(string authorization)
        {
            MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);
            HttpResponseMessage response = MiscFunctions.HttpClient.GetAsync("https://scriptblox.com/api/ping/idle").Result;
            MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return new BoolStatus(response.IsSuccessStatusCode, response.StatusCode.ToString());
        }

        /// <summary>
        /// Changes the username of the user.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="newUsername">The new username.</param>
        /// <returns>A BoolStatus object indicating if the username change was successful and the response text.</returns>
        public static BoolStatus ChangeUsername(string authorization, string newUsername)
        {
            if (UserMethods.IsUsernameTaken(newUsername)) return new BoolStatus(false, "Username taken.");

            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/update", authorization, $"{{\"username\":\"{newUsername}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Password updated!"), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Changes the password of the user.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A BoolStatus object indicating if the password change was successful and the response text.</returns>
        public static BoolStatus ChangePassword(string authorization, string oldPassword, string newPassword)
        {
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/change-password", authorization, $"{{\"oldPassword\":\"{oldPassword}\",\"newPassword\":\"{newPassword}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Password updated!"), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Updates the bio of the user.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="newBio">The new bio.</param>
        /// <returns>A BoolStatus object indicating if the bio update was successful and the response text.</returns>
        public static BoolStatus UpdateBio(string authorization, string newBio)
        {
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/update", authorization, $"{{\"bio\":\"{newBio}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("User updated!"), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Follows a user on ScriptBlox.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="username">The username of the user to follow.</param>
        /// <returns>A BoolStatus object indicating if the follow operation was successful and the response text.</returns>
        public static BoolStatus FollowUser(string authorization, string username)
        {
            string UserId = UserMethods.GetUserIdFromName(username);
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/follow", authorization, $"{{\"userId\":\"{UserId}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("You're now following "), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Unfollows a user on ScriptBlox.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="username">The username of the user to unfollow.</param>
        /// <returns>A BoolStatus object indicating if the unfollow operation was successful and the response text.</returns>
        public static BoolStatus UnFollowUser(string authorization, string username)
        {
            string UserId = UserMethods.GetUserIdFromName(username);
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/unfollow", authorization, $"{{\"userId\":\"{UserId}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Unfollowed user "), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Sends a password reset request.
        /// </summary>
        /// <param name="emailToReset">The email address of the user to reset the password for.</param>
        /// <returns>A BoolStatus object indicating if the password reset request was successful and the response text.</returns>
        public static BoolStatus SendPasswordReset(string emailToReset)
        {
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/reset-password", "", $"{{\"email\":\"{emailToReset}\"}}", false);

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Password reset link has been sent to your email!"), response.Content.ReadAsStringAsync().Result);
        }

        internal static HttpResponseMessage SendRequest(string url, string authorization, string data, bool WithAuth = true)
        {
            if (WithAuth)
                MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);

            HttpResponseMessage response = MiscFunctions.HttpClient.PostAsync(url, new StringContent(data)).Result;

            if (WithAuth)
                MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return response;
        }

    }
}
