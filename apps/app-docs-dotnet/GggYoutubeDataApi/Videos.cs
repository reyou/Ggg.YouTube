using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;
using Google.Apis.Http;

namespace GggYoutubeDataApi
{
    public class Videos
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        static Videos()
        {
            HttpMessageHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy()
                {
                    Address = new Uri("http://localhost.foodception.com:8888")
                }
            };
            HttpClient = new HttpClient(handler);
        }
        /// <summary>
        /// https://www.googleapis.com/youtube/v3/search?key={your_key_here}&channelId={channel_id_here}&part=snippet,id&order=date&maxResults=20
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetChannelVideosAsync(string channelId, string part = "", string fields = "",
            string nextPageToken = "")
        {
            StringBuilder url = new StringBuilder(YouTubeApi.ApiBase);
            url.Append("/search?");
            url.Append("key=");
            url.Append(CredentialsManager.GetApiKey());
            url.Append("&channelId=");
            url.Append(channelId);
            url.Append("&order=");
            url.Append("date");
            url.Append("&maxResults=");
            url.Append(50);
            if (!string.IsNullOrEmpty(nextPageToken))
            {
                url.Append("&pageToken=");
                url.Append(nextPageToken);
            }
            if (!string.IsNullOrEmpty(fields))
            {
                url.Append("&fields=");
                url.Append(fields);
            }
            if (!string.IsNullOrEmpty(part))
            {
                url.Append("&part=");
                url.Append(part);
            }
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(url.ToString());
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }
        public static async Task<string> GetVideo(string id, string part = "", string fields = "")
        {
            StringBuilder url = new StringBuilder(YouTubeApi.ApiBase);
            url.Append("/videos?");
            url.Append("id=");
            url.Append(id);
            url.Append("&key=");
            url.Append(CredentialsManager.GetApiKey());
            if (!string.IsNullOrEmpty(fields))
            {
                url.Append("&fields=");
                url.Append(fields);
            }
            if (!string.IsNullOrEmpty(part))
            {
                url.Append("&part=");
                url.Append(part);
            }
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(url.ToString());
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }

    }
}
