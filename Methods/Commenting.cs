using Newtonsoft.Json.Linq;
using ScriptBloxAPI.DataTypes;
using System.Collections.Generic;
using System.Net.Http;
using ScriptBloxAPI.Backend_Functions;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Methods
{
    public class Commenting
    {
        #region Non Async

        /// <summary>
        /// Adds a comment to the specified script using ScriptBlox.
        /// </summary>
        /// <param name="script">The script object to add the comment to.</param>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="comment">The text of the comment.</param>
        /// <returns>The newly added CommentObject.</returns>
        public static CommentObject AddComment(ScriptObject script, string authorization, string comment)
        {
            HttpResponseMessage response = SendRequest("https://scriptblox.com/api/comment/add", authorization, $"{{\"scriptId\":\"{script.Id}\",\"text\":\"{comment}\"}}");

            JToken jsonReturn = JToken.Parse(response.Content.ReadAsStringAsync().Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] == null)
                throw new ScriptBloxException("An error has occurred while adding comments: message <null>");
            if (jsonReturn.Value<string>("message") != "Commented!")
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));

            JToken commentData = jsonReturn["comment"];

            return new CommentObject(
                commentData?.Value<string>("_id") ?? "-1",
                commentData?.Value<string>("text") ?? "-1",
                0,
                0,
                UserMethods.GetUserFromUserId(commentData?.Value<string>("commentBy"))
            );

        }    
        
        /// <summary>
        /// Deletes a comment with the specified comment ID using the ScriptBlox API.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>A BoolStatus indicating whether the comment deletion was successful.</returns>
        public static BoolStatus DeleteComment(string authorization, string commentId)
        {
            MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);
            HttpResponseMessage response = MiscFunctions.HttpClient.DeleteAsync($"https://scriptblox.com/api/comment/{commentId}").Result;
            MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return new BoolStatus(response.IsSuccessStatusCode, response.Content.ReadAsStringAsync().Result);
        }

        
        /// <summary>
        /// Retrieves the comments associated with a script from ScriptBlox.
        /// </summary>
        /// <param name="script">The ScriptObject representing the script.</param>
        /// <returns>A list of CommentObject representing the comments associated with the script.</returns>
        /// <exception cref="ScriptBloxException">
        /// Thrown when an error occurs while fetching the JSON, when the API returns an error message, or when a backend error occurs.
        /// </exception>
        public static List<CommentObject> GetCommentsFromScript(ScriptObject script)
        {
            List<CommentObject> comments = new List<CommentObject>();

            JToken jsonReturn = JToken.Parse(MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/comment/{script.Id}?page=1&max=999").Result);

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["comments"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            foreach (JToken comment in jsonReturn["comments"])
            {
                comments.Add(new CommentObject(
                    comment.Value<string>("_id"),
                    comment.Value<string>("text"),
                    comment.Value<int>("likeCount"),
                    comment.Value<int>("dislikeCount"),
                    UserMethods.GetUserFromUsername(comment["commentBy"]?.Value<string>("username"))
                ));
            }

            return comments;
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
        #region Async

        /// <summary>
        /// Retrieves the comments associated with a script from ScriptBlox.
        /// </summary>
        /// <param name="script">The ScriptObject representing the script.</param>
        /// <returns>A list of CommentObject representing the comments associated with the script.</returns>
        /// <exception cref="ScriptBloxException">
        /// Thrown when an error occurs while fetching the JSON, when the API returns an error message, or when a backend error occurs.
        /// </exception>
        public static async Task<List<CommentObject>> GetCommentsFromScriptAsync(ScriptObject script)
        {
            List<CommentObject> comments = new List<CommentObject>();

            JToken jsonReturn = JToken.Parse(await MiscFunctions.HttpClient.GetStringAsync($"https://scriptblox.com/api/comment/{script.Id}?page=1&max=999"));

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the JSON, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["comments"] == null)
                throw new ScriptBloxException("Backend error occurred.");

            foreach (JToken comment in jsonReturn["comments"])
            {
                comments.Add(new CommentObject(
                    comment.Value<string>("_id"),
                    comment.Value<string>("text"),
                    comment.Value<int>("likeCount"),
                    comment.Value<int>("dislikeCount"),
                    await UserMethods.GetUserFromUsernameAsync(comment["commentBy"]?.Value<string>("username"))
                ));
            }

            return comments;
        }

        /// <summary>
        /// Deletes a comment with the specified comment ID using the ScriptBlox API.
        /// </summary>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>A BoolStatus indicating whether the comment deletion was successful.</returns>
        public static async Task<BoolStatus> DeleteCommentAsync(string authorization, string commentId)
        {
            MiscFunctions.HttpClient.DefaultRequestHeaders.Add("authorization", authorization);
            HttpResponseMessage response = await MiscFunctions.HttpClient.DeleteAsync($"https://scriptblox.com/api/comment/{commentId}");
            MiscFunctions.HttpClient.DefaultRequestHeaders.Remove("authorization");

            return new BoolStatus(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Adds a comment to the specified script using ScriptBlox.
        /// </summary>
        /// <param name="script">The script object to add the comment to.</param>
        /// <param name="authorization">The authorization token.</param>
        /// <param name="comment">The text of the comment.</param>
        /// <returns>The newly added CommentObject.</returns>
        public static async Task<CommentObject> AddCommentAsync(ScriptObject script, string authorization, string comment)
        {
            HttpResponseMessage response = await SendRequestAsync("https://scriptblox.com/api/comment/add", authorization, $"{{\"scriptId\":\"{script.Id}\",\"text\":\"{comment}\"}}");

            JToken jsonReturn = JToken.Parse(await response.Content.ReadAsStringAsync());

            if (jsonReturn == null)
                throw new ScriptBloxException("An error has occurred while fetching the JSON, please submit a bug report.");
            if (jsonReturn["message"] == null)
                throw new ScriptBloxException("An error has occurred while adding comments: message <null>");
            if (jsonReturn.Value<string>("message") != "Commented!")
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));

            JToken commentData = jsonReturn["comment"];

            return new CommentObject(
                commentData?.Value<string>("_id") ?? "-1",
                commentData?.Value<string>("text") ?? "-1",
                0,
                0,
                await UserMethods.GetUserFromUserIdAsync(commentData?.Value<string>("commentBy"))
            );
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
