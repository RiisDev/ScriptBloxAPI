using Newtonsoft.Json.Linq;
using ScriptBloxAPI.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Methods
{
    public class User
    {
        private UserObject _invalidUser = new UserObject(-1, -1, -1, "N/A", "N/A", "N/A", "N/A", "2023-05-22T10:36:23.273Z", "2023-05-22T10:36:23.273Z", false);

        /// <summary>
        /// Retrieves user information from the ScriptBlox API based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>A UserObject representing the user's information.</returns>
        /// <exception cref="ScriptBloxException">Thrown when an error occurs while fetching user information.</exception>
        public UserObject GetUserFromUsername(string username)
        {
            dynamic jsonReturn = MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/info/{username}").Result;

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn["message"]);
            if (jsonReturn["user"] == null)
                throw new ScriptBloxException("Backend error occured.");

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
        /// Checks if a username is taken by making a request to the ScriptBlox API.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username is taken; otherwise, false.</returns>
        public bool IsUsernameTaken(string username)
        {
            return MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/info/{username}").Result.Contains("true");
        }
    }
}
