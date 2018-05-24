using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggYoutubeDataApi
{
    /// <summary>
    /// https://developers.google.com/youtube/v3/getting-started
    /// </summary>
    [TestClass]
    public class VideosTests
    {
        [TestMethod]
        public async Task GetVideo()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithPart(id, part);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.WriteLine(videoResponse.ToJsonString());
            LoggingManager.CreateFile("GetVideo.txt", videoResponse.ToJsonString());
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideoWithFields2()
        {
            string id = "7lCDEYXw3mM";
            string fields = "items(id,snippet,statistics)";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithFields(id, fields, part);
            LoggingManager.WriteLine(result);
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideoWithFields()
        {
            string id = "7lCDEYXw3mM";
            string fields = "items(id,snippet,statistics)";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithFields(id, fields, part);
            LoggingManager.WriteLine(result);
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideoWithPart2()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithPart(id, part);
            LoggingManager.WriteLine(result);
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideoWithPart()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,contentDetails,statistics,status";
            string result = await Videos.GetVideoWithPart(id, part);
            LoggingManager.WriteLine(result);
            Debugger.Break();
        }
    }
}
