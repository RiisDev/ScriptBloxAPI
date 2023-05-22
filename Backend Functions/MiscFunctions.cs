using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI
{
    internal class MiscFunctions
    {
        internal static HttpClient HttpClient = new HttpClient();

        internal static DateTime ConvertStringToDateTime(string dateString)
        {
            string format = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
            DateTime result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            result = result.ToLocalTime();
            return result;
        }
    }
}
