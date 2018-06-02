using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GggYoutubeDataApiConsoleApp
{
    public class Search
    {
        [STAThread]
        public static void Main2(string[] args)
        {
            Console.WriteLine("YouTube Data API: Search");
            Console.WriteLine("========================");
            try
            {
                new Search().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private async Task Run()
        {
            BaseClientService.Initializer baseClientServiceInitializer = new BaseClientService.Initializer
            {
                ApiKey = CredentialsManager.GetApiKey(),
                ApplicationName = GetType().ToString()
            };
            YouTubeService youtubeService = new YouTubeService(baseClientServiceInitializer);

            SearchResource.ListRequest searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "Google"; // Replace with your search term.
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            SearchListResponse searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (SearchResult searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }
            Console.WriteLine("Videos:\n{0}\n", string.Join("\n", videos));
            Console.WriteLine("Channels:\n{0}\n", string.Join("\n", channels));
            Console.WriteLine("Playlists:\n{0}\n", string.Join("\n", playlists));

        }
    }
}
