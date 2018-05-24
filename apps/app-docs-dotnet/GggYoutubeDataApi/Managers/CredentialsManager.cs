using System.IO;

namespace GggYoutubeDataApi
{
    public class CredentialsManager
    {
        public static string GetApiKey()
        {
            string apiKey = File.ReadAllText("D:/apikeys/youtube/apikey.txt");
            return apiKey;
        }
    }
}