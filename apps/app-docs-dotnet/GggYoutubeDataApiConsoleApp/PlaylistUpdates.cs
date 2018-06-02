using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GggYoutubeDataApi;
using GggYoutubeDataApi.Managers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace GggYoutubeDataApiConsoleApp
{
    /// <summary>
    /// https://developers.google.com/youtube/v3/code_samples/dotnet
    /// https://github.com/youtube/api-samples
    /// </summary>
    public class PlaylistUpdates
    {
        [STAThread]
        public static void Main2(string[] args)
        {
            Console.WriteLine("YouTube Data API: Playlist Updates");
            Console.WriteLine("==================================");
            try
            {
                new PlaylistUpdates().Run().Wait();
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
                ClientSecrets clientSecrets = GoogleClientSecrets.Load(stream).Secrets;
                // This OAuth 2.0 access scope allows for full read/write access to the
                // authenticated user's account.
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                        new[] { YouTubeService.Scope.Youtube },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(GetType().ToString())
                );
            }
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            });

            // Create a new, private playlist in the authorized user's channel.
            Playlist newPlaylist = new Playlist
            {
                Snippet = new PlaylistSnippet
                {
                    Title = "Test Playlist",
                    Description = "A playlist created with the YouTube API v3"
                },
                Status = new PlaylistStatus
                {
                    PrivacyStatus = "public"
                }
            };
            newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

            // Add a video to the newly created playlist.
            PlaylistItem newPlaylistItem = new PlaylistItem
            {
                Snippet = new PlaylistItemSnippet
                {
                    PlaylistId = newPlaylist.Id,
                    ResourceId = new ResourceId
                    {
                        Kind = "youtube#video",
                        VideoId = "GNRMeaz6QRI"
                    }
                }
            };
            newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();
            Console.WriteLine("Playlist item id {0} was added to playlist id {1}.", newPlaylistItem.Id, newPlaylist.Id);
        }
    }
}
