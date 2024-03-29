﻿using System;
using Newtonsoft.Json.Linq;
using ScriptBloxAPI.DataTypes;
using System.Collections.Generic;
using System.Linq;
using ScriptBloxAPI.Backend_Functions;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global
#pragma warning disable IDE0270

namespace ScriptBloxAPI.Methods
{
    public static class ScriptsMethods
    {

        public enum FilterType
        {
            Hot,
            Verified,
            Unverified,
            Newest,
            Oldest,
            MostViewed,
            LeastViewed,
            Free,
            Paid
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

        private static IEnumerable<string> GetSlugsFromResults(JToken json)
        {
            List<string> slugs = new();

            try
            {
                json = JToken.Parse(json.ToString());

                if (!json.HasValues) return slugs;
                if (json["result"] == null) return slugs;
                if (json["result"]["scripts"] == null) return slugs;

                JArray scripts = json["result"]?["scripts"]?.ToObject<JArray>() ?? new JArray();

                slugs.AddRange(scripts.Select(script => script.Value<string>("slug")));
            }
            catch (Exception ex)
            {
                throw new ScriptBloxException($"An error occurred in 'GetSlugsFromResults' please create an issue @ github: {ex}\n{ex.StackTrace}");
            }

            return slugs;
        }

        private static ScriptObject CreateScriptFromData(JToken scriptData)
        {
            GameObject game = new(scriptData["game"].Value<long>("gameId"),
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
        #region Non Async
        /// <summary>
        /// Retrieves a script from ScriptBlox based on the provided ScriptBlox ID.
        /// </summary>
        /// <param name="bloxId">The ScriptBlox ID of the script to retrieve.</param>
        /// <returns>The script retrieved from the API, or a default script if the retrieval fails or the data is invalid.</returns>
        public static ScriptObject GetScriptFromScriptbloxId(string bloxId)
        {
            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetSafeString($"https://scriptblox.com/api/script/{bloxId}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["script"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            JToken scriptData = jsonReturn["script"];

            if (IsScriptDataInvalid(scriptData))
                throw new ScriptBloxException("An error has occurred while parsing the json, please submit a bug report.");

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

            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetSafeString($"https://scriptblox.com/api/script/fetch?page={pageNumber}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn);

            return slugsToCheck.Select(GetScriptFromScriptbloxId).ToList();
        }

        /// <summary>
        /// Retrieves a list of scripts from the front page based on the provided page number.
        /// </summary>
        /// <param name="pageNumber">The page number of the front page scripts (default is 1).</param>
        /// <returns>A list of Script objects representing the scripts from the front page.</returns>
        public static List<ScriptObject> GetScriptsFromPageNumber(int pageNumber = 1) => GetFrontPageScripts(pageNumber);

        /// <summary>
        /// Retrieves a list of scripts from ScriptBlox based on the provided search query and maximum results.
        /// </summary>
        /// <param name="searchQuery">The search query to filter the scripts.</param>
        /// <param name="maxResults">The maximum number of results to retrieve (default is 20).</param>
        /// <returns>A list of Script objects representing the scripts matching the search query.</returns>
        public static List<ScriptObject> GetScriptsFromQuery(string searchQuery, int maxResults = 20)
        {
            if (maxResults < 1) maxResults = 1;

            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetSafeString($"https://scriptblox.com/api/script/search?q={searchQuery}&page=1&max={maxResults}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn);

            return slugsToCheck.Select(GetScriptFromScriptbloxId).ToList();

        }

        /// <summary>
        /// Retrieves a list of scripts from ScriptBlox based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user whose scripts to retrieve.</param>
        /// <returns>A list of Script objects representing the scripts owned by the user.</returns>
        public static List<ScriptObject> GetScriptsFromUser(string username)
        {
            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetSafeString($"https://scriptblox.com/api/user/scripts/{username}?page=1"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn.ToString());

            return slugsToCheck.Select(GetScriptFromScriptbloxId).ToList();
        }

        /// <summary>
        /// Retrieves a list of ScriptObjects based on the specified filter type asynchronously.
        /// </summary>
        /// <param name="filterType">The type of filter to apply.</param>
        /// <returns>A list of ScriptObjects that match the specified filter type.</returns>
        public static List<ScriptObject> GetScriptsWithFilter(FilterType filterType)
        {
            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetSafeString($@"https://scriptblox.com/api/script/fetch?page=1&filters[]={filterType.ToString().ToLower()}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");
            
            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn.ToString());

            return slugsToCheck.Select(GetScriptFromScriptbloxId).ToList();
        }
        
#endregion
        #region Async

        /// <summary>
        /// Retrieves a script from ScriptBlox based on the provided ScriptBlox ID.
        /// </summary>
        /// <param name="bloxId">The ScriptBlox ID of the script to retrieve.</param>
        /// <returns>The script retrieved from the API, or a default script if the retrieval fails or the data is invalid.</returns>
        public static async Task<ScriptObject> GetScriptFromScriptbloxIdAsync(string bloxId)
        {
            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetSafeStringAsync($"https://scriptblox.com/api/script/{bloxId}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["script"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            JToken scriptData = jsonReturn["script"];

            if (IsScriptDataInvalid(scriptData))
                throw new ScriptBloxException("An error has occurred while parsing the json, please submit a bug report.");

            return CreateScriptFromData(scriptData);
        }

        /// <summary>
        /// Retrieves a list of scripts from the front page based on the provided page number.
        /// </summary>
        /// <param name="pageNumber">The page number of the front page scripts (default is 1).</param>
        /// <returns>A list of Script objects representing the scripts from the front page.</returns>
        public static async Task<List<ScriptObject>> GetFrontPageScriptsAsync(int pageNumber = 1)
        {
            if (pageNumber < 1) pageNumber = 1;

            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetSafeStringAsync($"https://scriptblox.com/api/script/fetch?page={pageNumber}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn);

            List<Task<ScriptObject>> scriptTasks = slugsToCheck.Select(GetScriptFromScriptbloxIdAsync).ToList();

            ScriptObject[] scripts = await Task.WhenAll(scriptTasks);

            return scripts.ToList();
        }


        public static async Task<List<ScriptObject>> GetScriptsFromPageNumberAsync(int pageNumber = 1) => await GetFrontPageScriptsAsync(pageNumber);

        /// <summary>
        /// Retrieves a list of scripts from ScriptBlox based on the provided search query and maximum results.
        /// </summary>
        /// <param name="searchQuery">The search query to filter the scripts.</param>
        /// <param name="maxResults">The maximum number of results to retrieve (default is 20).</param>
        /// <returns>A list of Script objects representing the scripts matching the search query.</returns>
        public static async Task<List<ScriptObject>> GetScriptsFromQueryAsync(string searchQuery, int maxResults = 20)
        {
            if (maxResults < 1) maxResults = 1;

            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetSafeStringAsync($"https://scriptblox.com/api/script/search?q={searchQuery}&page=1&max={maxResults}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn);

            List<Task<ScriptObject>> scriptTasks = slugsToCheck.Select(GetScriptFromScriptbloxIdAsync).ToList();

            ScriptObject[] scripts = await Task.WhenAll(scriptTasks);

            return scripts.ToList();
        }

        /// <summary>
        /// Retrieves a list of scripts from ScriptBlox based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user whose scripts to retrieve.</param>
        /// <returns>A list of Script objects representing the scripts owned by the user.</returns>
        public static async Task<List<ScriptObject>> GetScriptsFromUserAsync(string username)
        {
            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetSafeStringAsync($"https://scriptblox.com/api/user/scripts/{username}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn.ToString());

            List<Task<ScriptObject>> scriptTasks = slugsToCheck.Select(GetScriptFromScriptbloxIdAsync).ToList();

            ScriptObject[] scripts = await Task.WhenAll(scriptTasks);

            return scripts.ToList();
        }

        /// <summary>
        /// Retrieves a list of ScriptObjects based on the specified filter type asynchronously.
        /// </summary>
        /// <param name="filterType">The type of filter to apply.</param>
        /// <returns>A list of ScriptObjects that match the specified filter type.</returns>
        public static async Task<List<ScriptObject>> GetScriptsWithFilterAsync(FilterType filterType)
        {
            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetSafeStringAsync($@"https://scriptblox.com/api/script/fetch?page=1&filters[]={filterType.ToString().ToLower()}"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["result"]?["scripts"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            IEnumerable<string> slugsToCheck = GetSlugsFromResults(jsonReturn.ToString());

            List<Task<ScriptObject>> scriptTasks = slugsToCheck.Select(GetScriptFromScriptbloxIdAsync).ToList();

            ScriptObject[] scripts = await Task.WhenAll(scriptTasks);

            return scripts.ToList();
        }


        #endregion
    }
}
