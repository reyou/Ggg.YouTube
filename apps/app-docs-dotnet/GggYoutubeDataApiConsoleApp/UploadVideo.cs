using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GggYoutubeDataApi.Managers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GggYoutubeDataApiConsoleApp
{
    /// <summary>
    /// https://developers.google.com/youtube/v3/code_samples/dotnet
    /// </summary>
    public class UploadVideo
    {
        [STAThread]
        public static void Main2(string[] args)
        {
            Console.WriteLine("YouTube Data API: Upload Video");
            Console.WriteLine("==============================");

            try
            {
                new UploadVideo().Run().Wait();
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
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });
            Video video = new Video
            {
                Snippet = new VideoSnippet
                {
                    Title = "Default Video Title",
                    Description = "Default Video Description",
                    Tags = new[] { "tag1", "tag2" },
                    CategoryId = "22"
                }
            };
            // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus
            {
                PrivacyStatus = "unlisted"
            };
            // or "private" or "public"
            string filePath = AssetsManager.GetSampleVideoPath(); // Replace with path to actual movie file.
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                VideosResource.InsertMediaUpload videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += VideosInsertRequestProgressChanged;
                videosInsertRequest.ResponseReceived += VideosInsertRequestResponseReceived;

                await videosInsertRequest.UploadAsync();
            }

            void VideosInsertRequestProgressChanged(IUploadProgress progress)
            {
                switch (progress.Status)
                {
                    case UploadStatus.Uploading:
                        Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                        break;

                    case UploadStatus.Failed:
                        Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                        break;
                }
            }

            void VideosInsertRequestResponseReceived(Video videoParam)
            {
                Console.WriteLine("Video id '{0}' was successfully uploaded.", videoParam.Id);
            }

        }
    }
}
