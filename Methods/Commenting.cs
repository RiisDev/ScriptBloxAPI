using Newtonsoft.Json.Linq;
using ScriptBloxAPI.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Methods
{
    public class Commenting
    {
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
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] == null)
                

            if (jsonReturn.Value<string>("message") != "Commented!")
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));

            JToken commentData = jsonReturn["comment"];

            return new CommentObject(
                commentData.Value<string>("_id"),
                commentData.Value<string>("text"),
                0,
                0,
                UserMethods.GetUserFromUserId(commentData.Value<string>("commentBy"))
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
                throw new ScriptBloxException("An error has occured while fetching the json, please submit a bug report.");
            if (jsonReturn["message"] != null)
                throw new ScriptBloxException(jsonReturn.Value<string>("message"));
            if (jsonReturn["comments"] == null)
                throw new ScriptBloxException("Backend error occured.");

            foreach (JToken comment in jsonReturn["comments"])
            {
                comments.Add(new CommentObject(
                    comment.Value<string>("_id"),
                    comment.Value<string>("text"),
                    comment.Value<int>("likeCount"),
                    comment.Value<int>("dislikeCount"),
                    UserMethods.GetUserFromUsername(comment["commentBy"].Value<string>("username"))
                ));
            }

            return comments;
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
