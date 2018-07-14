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
    public class VideosProviderTests
    {
        [TestMethod]
        public async Task SearchChannelsAsync()
        {
            string part = "snippet,id";
            string keyword = "food";
            string result = await VideosProvider.SearchChannelsAsync(keyword, part);
            ChannelSearchResponse channelSearchResponse = ChannelSearchResponse.FromString(result);
            List<ChannelItem> items = channelSearchResponse.items;
            Console.WriteLine(items);
            LoggingManager.CreateFile("SearchChannelsAsync.txt", channelSearchResponse.Serialize());
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetChannelAsync()
        {
            string channelId = "UCJFp8uSYCjXOMnkUyb3CQ3Q";
            string part = "snippet,id";
            string result = await VideosProvider.GetChannelAsync(channelId, part);
            ChannelSearchResponse channelSearchResponse = ChannelSearchResponse.FromString(result);
            List<ChannelItem> items = channelSearchResponse.items;
            List<string> itemKinds = items.Select(q => q.id.kind).Distinct().ToList();
            Console.WriteLine(itemKinds);
            Console.WriteLine(items);
            LoggingManager.CreateFile("GetChannelAsync.txt", channelSearchResponse.Serialize());
            Debugger.Break();
        }

        [TestMethod]
        public async Task GetChannelVideosAsyncPaged()
        {
            string channelId = "UCJFp8uSYCjXOMnkUyb3CQ3Q";

            int targetCount = 250;
            int receivedCount = 0;

            string nextPageToken = "";
            List<ChannelItem> allChannelVideos = new List<ChannelItem>();
            do
            {
                string result = await VideosProvider.GetChannelVideosAsync(channelId, ChannelPartParams.Default, "", nextPageToken);
                ChannelSearchResponse channelSearchResponse = ChannelSearchResponse.FromString(result);
                if (targetCount > channelSearchResponse.pageInfo.totalResults)
                {
                    targetCount = channelSearchResponse.pageInfo.totalResults;
                }
                nextPageToken = channelSearchResponse.nextPageToken;
                List<ChannelItem> items = channelSearchResponse.items;
                receivedCount += items.Count;
                if (items.Any())
                {
                    allChannelVideos.AddRange(items);
                }
            } while (string.IsNullOrEmpty(nextPageToken) || receivedCount < targetCount);
            Console.WriteLine(allChannelVideos);
            Debugger.Break();
        }

        [TestMethod]
        public async Task GetChannelVideosAsync()
        {
            string channelId = "UCJFp8uSYCjXOMnkUyb3CQ3Q";
            string result = await VideosProvider.GetChannelVideosAsync(channelId);
            ChannelSearchResponse channelSearchResponse = ChannelSearchResponse.FromString(result);
            List<ChannelItem> items = channelSearchResponse.items;
            Console.WriteLine(items);
            LoggingManager.CreateFile("GetChannelVideosAsync.txt", channelSearchResponse.Serialize());
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideo()
        {
            string id = "7lCDEYXw3mM";
            string result = await VideosProvider.GetVideo(id, ChannelPartParams.Statistics);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.CreateFile("GetVideo.txt", videoResponse);
            Debugger.Break();
        }


        [TestMethod]
        public async Task GetVideoWithFields()
        {
            string id = "7lCDEYXw3mM";
            string result = await VideosProvider.GetVideo(id, ChannelPartParams.Statistics, ChannelFieldParams.Statistics);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.CreateFile("GetVideoWithFields.txt", videoResponse);
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideoWithPart2()
        {
            string id = "7lCDEYXw3mM";
            string result = await VideosProvider.GetVideo(id, ChannelPartParams.Statistics);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.CreateFile("GetVideoWithPart2.txt", videoResponse);
            Debugger.Break();
        }
        [TestMethod]
        public async Task GetVideoWithPart()
        {
            string id = "7lCDEYXw3mM";
            string result = await VideosProvider.GetVideo(id, ChannelPartParams.ContentDetails);
            VideoResponse videoResponse = VideoResponse.FromString(result);
            LoggingManager.CreateFile("GetVideoWithPart.txt", videoResponse);
            Debugger.Break();
        }
    }
}
