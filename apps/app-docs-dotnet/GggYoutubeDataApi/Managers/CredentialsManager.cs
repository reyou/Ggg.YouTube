using System.IO;

namespace GggYoutubeDataApi.Managers
{
    public class CredentialsManager
    {
        public static string GetApiKey()
        {
            string apiKey = File.ReadAllText("D:/apikeys/youtube/apikey.txt");
            return apiKey;
        }

        /// <summary>
        /// client_secrets.json
        /// </summary>
        /// <returns></returns>
        public static string GetClientSecretsLocation()
        {
            return
                "D:\\apikeys\\youtube\\client_secret_600994319952-5fbjg9423paiucuojuctet2ll1k7p1md.apps.googleusercontent.com.json";
        }
    }
}