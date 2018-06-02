using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;

namespace GggYoutubeDataApi
{
    public class Videos
    {
        public static async Task<string> GetVideo(string id, string part = "", string fields = "")
        {
            HttpClient client = new HttpClient();
            StringBuilder url = new StringBuilder("");
            url.Append(YouTubeApi.ApiBase);
            url.Append("?id=");
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
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url.ToString());
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }

    }
}
