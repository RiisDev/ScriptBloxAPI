using System;
using System.Globalization;
using System.Net.Http;
using System.Reflection;

namespace ScriptBloxAPI.Backend_Functions
{
    internal class MiscFunctions
    {

        internal static HttpClient InternalClient;
        internal static HttpClient HttpClient
        {
            get
            {
                if (InternalClient != null) return InternalClient;

                InternalClient = new HttpClient();
                InternalClient.DefaultRequestHeaders.UserAgent.ParseAdd(@$"iScriptBloxAPI/{Assembly.GetEntryAssembly()?.GetName().Version} (Windows 10/11; .NET Framework 4.8; C# <Latest>)");

                return InternalClient;
            }
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
