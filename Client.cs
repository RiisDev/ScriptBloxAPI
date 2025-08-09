using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ScriptBloxApi.Objects;

namespace ScriptBloxApi
{
    internal class Client
    {
        public class ScriptBloxInternalException(string message) : Exception(message);
        public class ScriptBloxApiException(string message) : Exception(message);

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

#nullable enable
        internal static async Task<T> Get<T>(string endpoint, (string Key, string Value)[]? queryParams)
        {
            string queryString = string.Join("&", queryParams?.Select(kvp => $"{kvp.Key}={kvp.Value}") ?? []);

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            HttpRequestMessage request = new(HttpMethod.Get, $"https://scriptblox.com/api/{endpoint}{queryString}");
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();
                (bool success, Error? data) = TryDeserialize<Error>(responseText);

                if (success && data is not null)
                    throw new ScriptBloxInternalException(data.Message);

                throw new ScriptBloxInternalException($"Error fetching: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
            }
                

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
                return (T)(object)jsonResponse;

            T? result = JsonSerializer.Deserialize<T>(jsonResponse);

            if (result is null)
                throw new ScriptBloxApiException("Deserialization returned null.");

            return result;
        }

        internal static (bool, T?) TryDeserialize<T>(string jsonResponse)
        {
            try
            {
                return (true, JsonSerializer.Deserialize<T>(jsonResponse));
            }
            catch 
            {
                return (false, default);
            }
        }
#nullable disable
    }
}
