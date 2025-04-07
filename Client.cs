using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScriptBloxApi
{
    internal class Client
    {
        private static readonly Lazy<HttpClient> LazyClient = new(() =>
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            client.DefaultRequestHeaders.Add("User-Agent", "IrisAgent ScriptBloxApi/1.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        });

        internal static HttpClient HttpClient => LazyClient.Value;

        internal static async Task<T> Get<T>(string endpoint, Dictionary<string, string> queryParams)
        {
            string queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            HttpRequestMessage request = new(HttpMethod.Get, $"https://scriptblox.com/api/{endpoint}{queryString}");
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(
                    $"Error fetching: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
                return (T)(object)jsonResponse;

            return JsonSerializer.Deserialize<T>(jsonResponse);
        }
    }
}
