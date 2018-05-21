using System;
using System.Threading.Tasks;
using GggYoutubeDataApi;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;

namespace GggYoutubeDataApiConsoleApp
{
    /// <summary>
    /// https://developers.google.com/api-client-library/dotnet/get_started
    /// </summary>
    class DiscoveryServiceSample
    {
        public static void Main2()
        {
            Console.WriteLine("Discovery API Sample");
            Console.WriteLine("====================");
            try
            {
                new DiscoveryServiceSample().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        /// BaseClientService: A base class for a client service which provides common 
        /// mechanism for all services, like  serialization and GZip support.  
        /// It should be safe to use a single service instance to make server requests
        /// concurrently from multiple threads.
        /// Initializer: An initializer class for the client service
        private async Task Run()
        {
            // Create the service.
            DiscoveryService service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = CredentialsManager.GetApiKey(),
            });
            // Run the request.
            Console.WriteLine("Executing a list request...");
            DirectoryList result = await service.Apis.List().ExecuteAsync();

            // Display the results.
            if (result.Items != null)
            {
                foreach (DirectoryList.ItemsData api in result.Items)
                {
                    Console.WriteLine(api.Id + " - " + api.Title);
                }
            }
        }

    }
}
