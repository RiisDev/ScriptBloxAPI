using ScriptBloxAPI.DataTypes;
using System.Net.Http;
using ScriptBloxAPI.Backend_Functions;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Methods
{
    public class Authorization
    {
        #region Non Async

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
            string userId = UserMethods.GetUserIdFromName(username);
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/follow", authorization, $"{{\"userId\":\"{userId}\"}}");

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
            string userId = UserMethods.GetUserIdFromName(username);
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/user/unfollow", authorization, $"{{\"userId\":\"{userId}\"}}");

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
        
        internal static HttpResponseMessage SendRequest(string url, string authorization, string data, bool withAuth = true)
        {
            if (withAuth)
                MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);

            HttpResponseMessage response = MiscFunctions.HttpClient.PostAsync(url, new StringContent(data)).Result;

            if (withAuth)
                MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return response;
        }

        #endregion
        #region Async Methods

        /// <summary>
        /// Sends a ping request to the idle endpoint asynchronously.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the request was successful and the response status code.</returns>
        public static async Task<BoolStatus> PingIdleAsync(string authorization)
        {
            MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);
            HttpResponseMessage response = await MiscFunctions.HttpClient.GetAsync("https://scriptblox.com/api/ping/idle");
            MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return new BoolStatus(response.IsSuccessStatusCode, response.StatusCode.ToString());
        }

        /// <summary>
        /// Changes the username of the user asynchronously.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="newUsername">The new username.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the username change was successful and the response text.</returns>
        public static async Task<BoolStatus> ChangeUsernameAsync(string authorization, string newUsername)
        {
            if (await UserMethods.IsUsernameTakenAsync(newUsername))
                return new BoolStatus(false, "Username taken.");

            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/user/update", authorization, $"{{\"username\":\"{newUsername}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Password updated!"), response.Content.ReadAsStringAsync().Result);
        }


        /// <summary>
        /// Changes the password of the user asynchronously.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the password change was successful and the response text.</returns>
        public static async Task<BoolStatus> ChangePasswordAsync(string authorization, string oldPassword, string newPassword)
        {
            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/user/change-password", authorization, $"{{\"oldPassword\":\"{oldPassword}\",\"newPassword\":\"{newPassword}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Password updated!"), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Updates the bio of the user asynchronously.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="newBio">The new bio.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the bio update was successful and the response text.</returns>
        public static async Task<BoolStatus> UpdateBioAsync(string authorization, string newBio)
        {
            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/user/update", authorization, $"{{\"bio\":\"{newBio}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("User updated!"), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Follows a user on ScriptBlox asynchronously.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="username">The username of the user to follow.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the follow operation was successful and the response text.</returns>
        public static async Task<BoolStatus> FollowUserAsync(string authorization, string username)
        {
            string userId = await UserMethods.GetUserIdFromNameAsync(username);
            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/user/follow", authorization, $"{{\"userId\":\"{userId}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("You're now following "), response.Content.ReadAsStringAsync().Result);
        }


        /// <summary>
        /// Unfollows a user on ScriptBlox asynchronously.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="username">The username of the user to unfollow.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the unfollow operation was successful and the response text.</returns>
        public static async Task<BoolStatus> UnFollowUserAsync(string authorization, string username)
        {
            string userId = await UserMethods.GetUserIdFromNameAsync(username);
            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/user/unfollow", authorization, $"{{\"userId\":\"{userId}\"}}");

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Unfollowed user "), response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Sends a password reset request asynchronously.
        /// </summary>
        /// <param name="emailToReset">The email address of the user to reset the password for.</param>
        /// <returns>A Task BoolStatus representing the asynchronous operation, indicating if the password reset request was successful and the response text.</returns>
        public static async Task<BoolStatus> SendPasswordResetAsync(string emailToReset)
        {
            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/user/reset-password", "", $"{{\"email\":\"{emailToReset}\"}}", false);

            return new BoolStatus(response.Content.ReadAsStringAsync().Result.Contains("Password reset link has been sent to your email!"), response.Content.ReadAsStringAsync().Result);
        }

        internal static async Task<HttpResponseMessage> SendRequestAsync(string url, string authorization, string data, bool withAuth = true)
        {
            if (withAuth)
                MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);

            HttpResponseMessage response = await MiscFunctions.HttpClient.PostAsync(url, new StringContent(data));

            if (withAuth)
                MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return response;
        }

        #endregion
    }
}
