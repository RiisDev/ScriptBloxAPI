using Newtonsoft.Json.Linq;
using ScriptBloxAPI.Backend_Functions;
using ScriptBloxAPI.DataTypes;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Methods
{
    public class UserMethods
    {
        #region Non Async

        /// <summary>
        /// Retrieves user information from the ScriptBlox API based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>A UserObject representing the user's information.</returns>
        /// <exception cref="ScriptBloxException">Thrown when an error occurs while fetching user information.</exception>
        public static UserObject GetUserFromUsername(string username)
        {
            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/info/{username}").Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["user"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            JToken userReturn = jsonReturn["user"];

            return new UserObject(
                userReturn.Value<int>("scriptsCount"),
                userReturn.Value<int>("followersCount"),
                userReturn.Value<int>("followingCount"),
                userReturn.Value<string>("username"),
                userReturn.Value<string>("bio"),
                userReturn.Value<string>("profilePicture"),
                userReturn.Value<string>("_id"),
                userReturn.Value<string>("createdAt"),
                userReturn.Value<string>("lastActive"),
                userReturn.Value<bool>("verified")
            );
        }

        /// <summary>
        /// Retrieves a UserObject based on the provided user ID using the ScriptBlox API.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The UserObject corresponding to the user ID.</returns>
        public static UserObject GetUserFromUserId(string userId) => GetUserFromUsername(userId);

        /// <summary>
        /// Retrieves the user ID from the given username using the ScriptBlox API.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user ID corresponding to the username.</returns>
        public static string GetUserIdFromName(string username)
        {
            return GetUserFromUsername(username).Id;
        }


        /// <summary>
        /// Checks if a username is taken by making a request to the ScriptBlox API.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username is taken; otherwise, false.</returns>
        public static bool IsUsernameTaken(string username)
        {
            return MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/info/{username}").Result.Contains("true");
        }

        #endregion
        #region Async

        /// <summary>
        /// Retrieves user information from the ScriptBlox API based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>A UserObject representing the user's information.</returns>
        /// <exception cref="ScriptBloxException">Thrown when an error occurs while fetching user information.</exception>
        public static async Task<UserObject> GetUserFromUsernameAsync(string username)
        {
            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/info/{username}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["user"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            JToken userReturn = jsonReturn["user"];

            return new UserObject(
                userReturn.Value<int>("scriptsCount"),
                userReturn.Value<int>("followersCount"),
                userReturn.Value<int>("followingCount"),
                userReturn.Value<string>("username"),
                userReturn.Value<string>("bio"),
                userReturn.Value<string>("profilePicture"),
                userReturn.Value<string>("_id"),
                userReturn.Value<string>("createdAt"),
                userReturn.Value<string>("lastActive"),
                userReturn.Value<bool>("verified")
            );
        }

        /// <summary>
        /// Retrieves a UserObject based on the provided user ID using the ScriptBlox API.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The UserObject corresponding to the user ID.</returns>
        public static Task<UserObject> GetUserFromUserIdAsync(string userId)
        {
            return GetUserFromUsernameAsync(userId);
        }

        /// <summary>
        /// Retrieves the user ID from the given username using the ScriptBlox API.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user ID corresponding to the username.</returns>
        public static async Task<string> GetUserIdFromNameAsync(string username)
        {
            return (await GetUserFromUsernameAsync(username)).Id;
        }

        /// <summary>
        /// Checks if a username is taken by making a request to the ScriptBlox API.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username is taken; otherwise, false.</returns>
        public static async Task<bool> IsUsernameTakenAsync(string username)
        {
            string result = await MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/info/{username}");
            return result.Contains("true");
        }


        #endregion
    }
}
