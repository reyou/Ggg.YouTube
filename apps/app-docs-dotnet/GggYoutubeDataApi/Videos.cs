using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GggYoutubeDataApi
{
    public class Videos
    {
        public static async Task<string> GetVideoWithFields(string id, string fields, string part)
        {
            HttpClient client = new HttpClient();
            StringBuilder url = new StringBuilder("");
            url.Append(YouTubeApi.ApiBase);
            url.Append("?id=");
            url.Append(id);
            url.Append("&key=");
            url.Append(CredentialsManager.GetApiKey());
            url.Append("&fields=");
            url.Append(fields);
            url.Append("&part=");
            url.Append(part);
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url.ToString());
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }
        public static async Task<string> GetVideoWithPart(string id, string part)
        {
            HttpClient client = new HttpClient();
            StringBuilder url = new StringBuilder("");
            url.Append(YouTubeApi.ApiBase);
            url.Append("?id=");
            url.Append(id);
            url.Append("&key=");
            url.Append(CredentialsManager.GetApiKey());
            url.Append("&part=");
            url.Append(part);
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url.ToString());
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }
    }
}
