using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net.Http;

namespace MusicApi.Models
{
    /// <summary>
    /// Static class for seeding the DB on Application Startup using the Audio Network API
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Method for seeding the DB on Application Startup using the Audio Network API.
        /// Uses IServiceProvider to fetch Required services Such as DB Config settings from the DI Container during startup
        /// </summary>        
        public static async void Initialise(IServiceProvider serviceProvider)
        {
            // Fetch Audio Network API Key from IConfig Service Provider
            IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
            var apiKey = config.GetValue<string>("AudioNetworkApiKey");

            // Fetch MongoDB Config settings 
            var musicTracksDatabaseSettings = serviceProvider.GetService<IOptions<MusicTracksDatabaseSettings>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>(); //Get Logger from Program.cs
            

            #region DBConfig
            // Initialise MongoDB Client and fetch DB collection
            var mongoClient = new MongoClient(
                    musicTracksDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                musicTracksDatabaseSettings.Value.DatabaseName);

            var tracksCollection = mongoDatabase.GetCollection<Track>(
                musicTracksDatabaseSettings.Value.TracksCollectionName);
            #endregion

            // Fetch tracks using Audio Network HTTP REST API
            var tracks = await FetchAllTracksFromApiAsync(apiKey, logger);

            try
            {
                // Use BulkWrite to Upsert records into the DB Collection. 
                // Used Track.Id as primary key to either update or insert
                if (tracks is not null)
                {                  
                    var bulkOps = new List<WriteModel<Track>>();
                    foreach (var record in tracks)
                    {
                        var upsertOne = new ReplaceOneModel<Track>(
                            Builders<Track>.Filter.Where(x => x.Id == record.Id),
                            record)
                        { IsUpsert = true };
                        bulkOps.Add(upsertOne);
                    }
                    tracksCollection.BulkWrite(bulkOps);

                    logger.LogInformation("DB Seeding complete");
                }
                else
                {
                    logger.LogError($"Error fetching tracks from AudioNetwork API");
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Error seeding Database: {e}");
                throw;
            }

        }

        /// <summary>
        /// Fetch All tracks from Audio Network API
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="logger">Logger object for logging</param>
        /// <returns>List of all tracks. List<Track> </returns>
        private async static Task<List<Track>?> FetchAllTracksFromApiAsync(string apiKey, ILogger logger)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string baseUrl = $"https://musicapi.audionetwork.com";
                    string url = baseUrl + "/tracks?limit=500"; //Limiting tracks to 500 to keep things simple 

                    httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

                    var response = await httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {                        
                        logger.LogError($"Non success code when fetching tracks from AudioNetwork API. Status Code: {response.StatusCode} Reason: {response.ReasonPhrase}");
                        return null;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var tracks = JsonConvert.DeserializeObject<List<Track>>(content); // Deserialise to List<Track> object to make insertion to DB easier

                    if (tracks is null)
                    {
                        return null;
                    }

                    return tracks;
                }
                catch (Exception e)
                {                    
                    logger.LogError($"Error fetching tracks from AudioNetwork API: {e}");
                    throw;
                }
            }

        }
   }
        
}

