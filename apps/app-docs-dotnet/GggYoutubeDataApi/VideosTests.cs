using System.Diagnostics;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;
using GggYoutubeDataApi.Models;
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
            LoggingManager.CreateFile("GetVideo.txt", videoResponse);
        }
        [TestMethod]
        public async Task GetVideoWithFields2()
        {
            string id = "7lCDEYXw3mM";
            string fields = "items(id,snippet,statistics)";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithFields(id, fields, part);
            LoggingManager.CreateFile("GetVideoWithFields2.txt", result);
        }
        [TestMethod]
        public async Task GetVideoWithFields()
        {
            string id = "7lCDEYXw3mM";
            string fields = "items(id,snippet,statistics)";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithFields(id, fields, part);
            LoggingManager.CreateFile("GetVideoWithFields.txt", result);
        }
        [TestMethod]
        public async Task GetVideoWithPart2()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,statistics";
            string result = await Videos.GetVideoWithPart(id, part);
            LoggingManager.CreateFile("GetVideoWithPart2.txt", result);
        }
        [TestMethod]
        public async Task GetVideoWithPart()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,contentDetails,statistics,status";
            string result = await Videos.GetVideoWithPart(id, part);
            LoggingManager.CreateFile("GetVideoWithPart.txt", result);
        }
    }
}
