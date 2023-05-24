using Newtonsoft.Json.Linq;
using ScriptBloxAPI.DataTypes;
using System.Collections.Generic;

namespace ScriptBloxAPI.Methods
{
    public class ScriptsMethods
    {
        /// <summary>
        /// Retrieves a script from Scriptblox based on the provided Scriptblox ID.
        /// </summary>
        /// <param name="bloxId">The Scriptblox ID of the script to retrieve.</param>
        /// <returns>The script retrieved from the API, or a default script if the retrieval fails or the data is invalid.</returns>
        public static ScriptObject GetScriptFromScriptbloxId(string bloxId)
        {
            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/script/{bloxId}").Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["script"] == null)
                throw new ScriptBloxException("Backend error occured.");

            JToken scriptData = jsonReturn["script"];

            if (IsScriptDataInvalid(scriptData))
                throw new ScriptBloxException("An error has occured while parsing the json, please submit a bug report.");

            return CreateScriptFromData(scriptData);
        }
        
        /// <summary>
        /// Retrieves a list of scripts from the front page based on the provided page number.
        /// </summary>
        /// <param name="pageNumber">The page number of the front page scripts (default is 1).</param>
        /// <returns>A list of Script objects representing the scripts from the front page.</returns>
        public static List<ScriptObject> GetFrontPageScripts(int pageNumber = 1)
        {
            if (pageNumber < 1) pageNumber = 1;

            List<ScriptObject> scriptsToReturn = new List<ScriptObject>();

            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/script/fetch?page={pageNumber}").Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["scripts"] == null)
                throw new ScriptBloxException("Backend error occured.");

            List<string> slugsToCheck = GetSlugsFromResults(jsonReturn);

            foreach (string slug in slugsToCheck) scriptsToReturn.Add(GetScriptFromScriptbloxId(slug));

            return scriptsToReturn;
        }

        /// <summary>
        /// Retrieves a list of scripts from the front page based on the provided page number.
        /// </summary>
        /// <param name="pageNumber">The page number of the front page scripts (default is 1).</param>
        /// <returns>A list of Script objects representing the scripts from the front page.</returns>
        public static List<ScriptObject> GetScriptsFromPageNumber(int pageNumber = 1) => GetFrontPageScripts(pageNumber);

        /// <summary>
        /// Retrieves a list of scripts from Scriptblox based on the provided search query and maximum results.
        /// </summary>
        /// <param name="searchQuery">The search query to filter the scripts.</param>
        /// <param name="maxResults">The maximum number of results to retrieve (default is 20).</param>
        /// <returns>A list of Script objects representing the scripts matching the search query.</returns>
        public static List<ScriptObject> GetScriptsFromQuery(string searchQuery, int maxResults = 20)
        {
            if (maxResults < 1) maxResults = 1;

            List<ScriptObject> scriptsToReturn = new List<ScriptObject>();

            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/script/search?q={searchQuery}&page=1&max={maxResults}").Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["scripts"] == null)
                throw new ScriptBloxException("Backend error occured.");

            List<string> slugsToCheck = GetSlugsFromResults(jsonReturn);

            foreach (string slug in slugsToCheck) scriptsToReturn.Add(GetScriptFromScriptbloxId(slug));

            return scriptsToReturn;

        }

        /// <summary>
        /// Retrieves a list of scripts from Scriptblox based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user whose scripts to retrieve.</param>
        /// <returns>A list of Script objects representing the scripts owned by the user.</returns>
        public static List<ScriptObject> GetScriptsFromUser(string username)
        {
            List<ScriptObject> scriptsToReturn = new List<ScriptObject>();

            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/user/scripts/{username}?page=1").Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["scripts"] == null)
                throw new ScriptBloxException("Backend error occured.");

            List<string> slugsToCheck = GetSlugsFromResults(jsonReturn.ToString());

            foreach (string slug in slugsToCheck) scriptsToReturn.Add(GetScriptFromScriptbloxId(slug));

            return scriptsToReturn;
        }

        #region Internal Functions
        private static bool IsScriptDataInvalid(JToken scriptData)
        {
            return scriptData["game"] == null ||
                   scriptData["tags"] == null ||
                   scriptData["script"] == null ||
                   scriptData["views"] == null ||
                   scriptData["verified"] == null ||
                   scriptData["key"] == null ||
                   scriptData["isUniversal"] == null ||
                   scriptData["isPatched"] == null ||
                   scriptData["createdAt"] == null ||
                   scriptData["updatedAt"] == null ||
                   scriptData["likeCount"] == null ||
                   scriptData["dislikeCount"] == null ||
                   scriptData["slug"] == null ||
                   scriptData["id"] == null;
        }
        
        private static List<string> GetSlugsFromResults(JToken json)
        {
            List<string> slugs = new List<string>();

            JArray scripts = (JArray)json["result"]["scripts"];

            foreach (JToken script in scripts)
            {
                string slug = script.Value<string>("slug");
                slugs.Add(slug);
            }

            return slugs;
        }

        private static ScriptObject CreateScriptFromData(JToken scriptData)
        {
            GameObject game = new GameObject(scriptData["game"].Value<long>("gameId"),
                                             scriptData["game"].Value<string>("name"),
                                             scriptData["game"].Value<string>("imageUrl"));

            return new ScriptObject(game,
                              scriptData.Value<string>("title"),
                              scriptData.Value<string>("_id"),
                              scriptData.Value<string>("slug"),
                              scriptData.Value<string>("script"),
                              scriptData.Value<string>("createdAt"),
                              scriptData.Value<string>("updatedAt"),
                              scriptData.Value<long>("views"),
                              scriptData.Value<long>("likeCount"),
                              scriptData.Value<long>("dislikeCount"),
                              scriptData.Value<bool>("isUniversal"),
                              scriptData.Value<bool>("isPatched"),
                              scriptData.Value<bool>("key"),
                              scriptData.Value<bool>("verified"),
                              new List<string>());
        }
        #endregion
    }
}
