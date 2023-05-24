using System;
using System.Globalization;
using System.Net.Http;

namespace ScriptBloxAPI
{
    internal class MiscFunctions
    {
        internal static HttpClient HttpClient = new HttpClient();
        internal static DateTime ConvertStringToDateTime(string dateString, string timeFormat = "MM/dd/yyyy HH:mm:ss")
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
