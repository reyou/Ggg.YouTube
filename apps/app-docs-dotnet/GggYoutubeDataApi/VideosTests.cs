using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
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
        public async Task GetChannelVideosAsyncPaged()
        {
            string channelId = "UCJFp8uSYCjXOMnkUyb3CQ3Q";
            string part = "snippet,id";
            int targetCount = int.MaxValue;
            int receivedCount = 0;

            string nextPageToken = "";
            List<ChannelItem> allChannelVideos = new List<ChannelItem>();
            do
            {
                string result = await Videos.GetChannelVideosAsync(channelId, part, "", nextPageToken);
                ChannelSearchResponse channelSearchResponse = ChannelSearchResponse.FromString(result);
                if (targetCount > channelSearchResponse.pageInfo.totalResults)
                {
                    targetCount = channelSearchResponse.pageInfo.totalResults;
                }
                nextPageToken = channelSearchResponse.nextPageToken;
                List<ChannelItem> items = channelSearchResponse.items;
                List<ChannelItem> channelVideos = items.Where(q => q.id.kind.Equals("youtube#video", StringComparison.OrdinalIgnoreCase)).ToList();
                receivedCount += channelVideos.Count;
                if (channelVideos.Any())
                {
                    allChannelVideos.AddRange(channelVideos);
                }
            } while (string.IsNullOrEmpty(nextPageToken) || receivedCount < targetCount);
            Console.WriteLine(allChannelVideos);
        }

        [TestMethod]
        public async Task GetChannelVideosAsync()
        {
            string configvalue1 = ConfigurationManager.AppSettings["testKey"];
            Console.WriteLine(configvalue1);
            string channelId = "UCJFp8uSYCjXOMnkUyb3CQ3Q";
            string part = "snippet,id";
            string result = await Videos.GetChannelVideosAsync(channelId, part);
            ChannelSearchResponse channelSearchResponse = ChannelSearchResponse.FromString(result);
            List<ChannelItem> items = channelSearchResponse.items;
            List<string> itemKinds = items.Select(q => q.id.kind).Distinct().ToList();
            Console.WriteLine(itemKinds);
            List<ChannelItem> channelVideos = items.Where(q => q.id.kind.Equals("youtube#video", StringComparison.OrdinalIgnoreCase)).ToList();
            Console.WriteLine(channelVideos);
            LoggingManager.CreateFile("GetChannelVideosAsync.txt", channelSearchResponse.Serialize());
        }
        [TestMethod]
        public async Task GetVideo()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,statistics";
            string result = await Videos.GetVideo(id, part);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.CreateFile("GetVideo.txt", videoResponse);
        }

        [TestMethod]
        public async Task GetVideoNoPart()
        {
            string id = "7lCDEYXw3mM";
            string result = await Videos.GetVideo(id);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.CreateFile("GetVideoNoPart.txt", videoResponse);
        }
        [TestMethod]
        public async Task GetVideoWithFields()
        {
            string id = "7lCDEYXw3mM";
            string fields = "items(id,snippet,statistics)";
            string part = "snippet,statistics";
            string result = await Videos.GetVideo(id, part, fields);
            LoggingManager.CreateFile("GetVideoWithFields.txt", result);
        }
        [TestMethod]
        public async Task GetVideoWithPart2()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,statistics";
            string result = await Videos.GetVideo(id, part);
            LoggingManager.CreateFile("GetVideoWithPart2.txt", result);
        }
        [TestMethod]
        public async Task GetVideoWithPart()
        {
            string id = "7lCDEYXw3mM";
            string part = "snippet,contentDetails,statistics,status";
            string result = await Videos.GetVideo(id, part);
            LoggingManager.CreateFile("GetVideoWithPart.txt", result);
        }
    }
}
