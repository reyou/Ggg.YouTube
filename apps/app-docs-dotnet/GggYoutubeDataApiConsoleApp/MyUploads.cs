using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GggYoutubeDataApiConsoleApp
{
    /// <summary>
    /// https://developers.google.com/youtube/v3/code_samples/dotnet
    /// </summary>
    public class MyUploads
    {
        [STAThread]
        public static void Main2(string[] args)
        {
            Console.WriteLine("YouTube Data API: My Uploads");
            Console.WriteLine("============================");

            try
            {
                new MyUploads().Run().Wait();
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
            UserCredential credential;
            string clientSecretsPath = CredentialsManager.GetClientSecretsLocation();
            using (FileStream stream = new FileStream(clientSecretsPath, FileMode.Open, FileAccess.Read))
            {
                // This OAuth 2.0 access scope allows for read-only access to the authenticated 
                // user's account, but not other types of account access.
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.YoutubeReadonly },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(GetType().ToString())
                );
            }

            BaseClientService.Initializer baseClientServiceInitializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            };
            YouTubeService youtubeService = new YouTubeService(baseClientServiceInitializer);

            ChannelsResource.ListRequest channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;

            // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            ChannelListResponse channelsListResponse = await channelsListRequest.ExecuteAsync();
            foreach (Channel channel in channelsListResponse.Items)
            {
                // From the API response, extract the playlist ID that identifies the list
                // of videos uploaded to the authenticated user's channel.
                string uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

                Console.WriteLine("Videos in list {0}", uploadsListId);

                string nextPageToken = "";
                while (nextPageToken != null)
                {
                    PlaylistItemsResource.ListRequest playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                    playlistItemsListRequest.PlaylistId = uploadsListId;
                    playlistItemsListRequest.MaxResults = 50;
                    playlistItemsListRequest.PageToken = nextPageToken;

                    // Retrieve the list of videos uploaded to the authenticated user's channel.
                    PlaylistItemListResponse playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                    foreach (PlaylistItem playlistItem in playlistItemsListResponse.Items)
                    {
                        // Print information about each video.
                        Console.WriteLine("{0} ({1})", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId);
                    }

                    nextPageToken = playlistItemsListResponse.NextPageToken;
                }
            }

        }

    }
}
