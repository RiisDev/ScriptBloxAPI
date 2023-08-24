using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ScriptBloxAPI.Backend_Functions
{
    internal static class MiscFunctions
    {

        internal static HttpClient InternalClient;
        internal static HttpClient HttpClient
        {
            get
            {
                if (InternalClient != null) return InternalClient;

                InternalClient = new HttpClient();
                InternalClient.DefaultRequestHeaders.UserAgent.ParseAdd(@$"iScriptBloxAPI/{Assembly.GetEntryAssembly()?.GetName().Version} (Windows 10/11; .NET Framework {Environment.Version}; C# <Latest>)");

                return InternalClient;
            }
        }


        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
        public static async Task<string> GetSafeStringAsync(this HttpClient httpClient, string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ScriptBloxException(@$"The resource '{Path.GetFileNameWithoutExtension(url)}' could not be found.");
                case HttpStatusCode.BadRequest:
                    throw new ScriptBloxException(@"An error occurred while trying to perform the request. Please ensure your request is correctly formatted.");
                case HttpStatusCode.Forbidden:
                    throw new ScriptBloxException(@"The request was forbidden. You might have been rate-limited or lack proper permissions.");
                case HttpStatusCode.GatewayTimeout:
                    throw new ScriptBloxException(@"The gateway timed out while processing the request. Please check if ScriptBlox is currently online.");
                case HttpStatusCode.InternalServerError:
                    throw new ScriptBloxException(@"An internal server error has occurred on the server side. Please contact ScriptBloxAdmin for assistance.");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                JToken.Parse(responseBody);
            }
            catch (JsonException)
            {
                throw new ScriptBloxException($"Failed to parse JSON return.\n{responseBody}");
            }

            return responseBody;
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
        public static string GetSafeString(this HttpClient httpClient, string url)
        {
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ScriptBloxException(@$"The resource '{Path.GetFileNameWithoutExtension(url)}' could not be found.");
                case HttpStatusCode.BadRequest:
                    throw new ScriptBloxException(@"An error occurred while trying to perform the request. Please ensure your request is correctly formatted.");
                case HttpStatusCode.Forbidden:
                    throw new ScriptBloxException(@"The request was forbidden. You might have been rate-limited or lack proper permissions.");
                case HttpStatusCode.GatewayTimeout:
                    throw new ScriptBloxException(@"The gateway timed out while processing the request. Please check if ScriptBlox is currently online.");
                case HttpStatusCode.InternalServerError:
                    throw new ScriptBloxException(@"An internal server error has occurred on the server side. Please contact ScriptBloxAdmin for assistance.");
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;

            try
            {
                JToken.Parse(responseBody);
            }
            catch (JsonException)
            {
                throw new ScriptBloxException($"Failed to parse JSON return.\n{responseBody}");
            }

            return responseBody;
        }


        internal static DateTime ConvertStringToDateTime(string dateString, string timeFormat = @"MM/dd/yyyy HH:mm:ss")
        {
            try
            {
                DateTime result = DateTime.ParseExact(dateString, timeFormat, CultureInfo.InvariantCulture);
                result = result.ToLocalTime();
                return result;
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
