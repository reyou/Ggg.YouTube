using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;

namespace GggYoutubeDataApi
{
    public class VideosProvider
    {
        private static readonly HttpClient HttpClient;

        static VideosProvider()
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


        public static async Task<string> GetVideo(string id, string part = ChannelParts.Default, string fields = ChannelFields.Default)
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
        private static async Task<string> GetChannelItemsAsync(string type, ChannelSearchParams searchParams)
        {
            if (string.IsNullOrEmpty(type))
            {
                type = SearchTypes.Video;
            }
            StringBuilder url = new StringBuilder(YouTubeApi.ApiBase);
            url.Append("/search?");
            url.Append("key=");
            url.Append(CredentialsManager.GetApiKey());
            url.Append("&type=");
            url.Append(type);
            url.Append("&order=");
            url.Append("date");
            url.Append("&maxResults=");
            url.Append(50);
            if (!string.IsNullOrEmpty(searchParams.ChannelId))
            {
                url.Append("&channelId=");
                url.Append(searchParams.ChannelId);
            }
            if (!string.IsNullOrEmpty(searchParams.Keyword))
            {
                url.Append("&q=");
                url.Append(Uri.EscapeUriString(searchParams.Keyword));
            }
            if (!string.IsNullOrEmpty(searchParams.Fields))
            {
                url.Append("&fields=");
                url.Append(searchParams.Fields);
            }
            if (!string.IsNullOrEmpty(searchParams.Part))
            {
                url.Append("&part=");
                url.Append(searchParams.Part);
            }
            if (!string.IsNullOrEmpty(searchParams.NextPageToken))
            {
                url.Append("&pageToken=");
                url.Append(searchParams.NextPageToken);
            }
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(url.ToString());
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }


        /// <summary>
        /// matching video, channel, and playlist
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetChannelVideosAsync(string channelId, string part = ChannelParts.Default, string fields = ChannelFields.Default,
        string nextPageToken = "")
        {
            return await GetChannelItemsAsync(SearchTypes.Video, new ChannelSearchParams()
            {
                ChannelId = channelId,
                Part = part,
                Fields = fields,
                NextPageToken = nextPageToken
            });
        }

        public static async Task<string> GetChannelAsync(string channelId, string part, string fields = ChannelFields.Default,
        string nextPageToken = "")
        {
            return await GetChannelItemsAsync(SearchTypes.Channel, new ChannelSearchParams()
            {
                ChannelId = channelId,
                Part = part,
                Fields = fields,
                NextPageToken = nextPageToken
            });
        }

        public static async Task<string> SearchChannelsAsync(string keyword, string part, string fields = ChannelFields.Default,
        string nextPageToken = "")
        {
            return await GetChannelItemsAsync(SearchTypes.Channel, new ChannelSearchParams()
            {
                Keyword = keyword,
                Part = part,
                Fields = fields,
                NextPageToken = nextPageToken
            });
        }




    }
}
