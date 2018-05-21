using System.Diagnostics;

namespace GggYoutubeDataApi
{
    public class LoggingManager
    {
        public static void WriteLine(string result)
        {
            Debug.Write(result);
        }
    }
}