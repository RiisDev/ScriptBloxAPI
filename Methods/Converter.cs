﻿using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ScriptBloxAPI.Backend_Functions;

namespace ScriptBloxAPI.Methods
{
    public class Converter
    {
        public static async Task<Bitmap> ConvertWebPToBitmap(string fileUrl)
        {
            try
            {
                byte[] imageBytes = await MiscFunctions.HttpClient.GetByteArrayAsync(fileUrl);
                
                using StringContent imageJsonFormat = new(
                    $"{{\"image\": \"{Convert.ToBase64String(imageBytes)}\"}}", Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await MiscFunctions.HttpClient.PostAsync("https://scriptblox.com/api/user/image", imageJsonFormat);

                Bitmap finalBitmap =
                    new(new MemoryStream(Convert.FromBase64String(response.Content.ReadAsStringAsync().Result)));

                return response.IsSuccessStatusCode
                    ? finalBitmap
                    : throw new ScriptBloxException(
                        $"An error occurred while sending the request: {response.Content.ReadAsStringAsync().Result}");
            }
            catch (Exception ex)
            {
                throw new ScriptBloxException($"An error occurred while trying to handle conversion: {ex}");
            }
        }

        public static async Task<Bitmap> ConvertWebPToBitmapInternal(string webpUrl)
        {
            try
            {
                using HttpResponseMessage response = await MiscFunctions.HttpClient.GetAsync(webpUrl);
                response.EnsureSuccessStatusCode();

                using Stream contentStream = await response.Content.ReadAsStreamAsync();
                using MemoryStream webPStream = new();

                await contentStream.CopyToAsync(webPStream);

                using Bitmap bitmap = new(webPStream);
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new ScriptBloxException($"An error occurred while trying to handle conversion: {ex}");
            }
        }
    }
}
