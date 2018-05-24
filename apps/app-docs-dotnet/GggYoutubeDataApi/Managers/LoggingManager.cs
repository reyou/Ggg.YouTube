using System.Diagnostics;

namespace GggYoutubeDataApi
{
    public class LoggingManager
    {
        public static void WriteLine(string result)
        {
            Debug.Write(result);
        }

        public static void CreateFile(string getvideoTxt, string toJsonString)
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
        }
    }
}