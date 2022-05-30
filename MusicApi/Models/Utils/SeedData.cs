using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net.Http;

namespace MusicApi.Models
{
    public static class SeedData
    {
        public static async void Initialise(IServiceProvider serviceProvider)
        {
            // TODO: Add logging
            // Initialise(IOptions<MusicTracksDatabaseSettings> musicTracksDatabaseSettings, ILogger logger)
            IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
            var apiKey = config.GetValue<string>("AudioNetworkApiKey");

            var musicTracksDatabaseSettings = serviceProvider.GetService<IOptions<MusicTracksDatabaseSettings>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            

            #region DBConfig
            var mongoClient = new MongoClient(
                    musicTracksDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                musicTracksDatabaseSettings.Value.DatabaseName);

            var tracksCollection = mongoDatabase.GetCollection<Track>(
                musicTracksDatabaseSettings.Value.TracksCollectionName);
            #endregion

            var tracks = FetchAllTracksFromApiAsync(apiKey, logger).Result;

            //var filter = Builders<Track>.Filter.Empty;
            //var update = Builders<Track>.Update.;
            //tracksCollection.UpdateManyAsync(filter, tracks);

            //await tracksCollection.InsertManyAsync(tracks);           
            //await tracksCollection.UpdateManyAsync(filter, ,new UpdateOptions() { IsUpsert = true }); 

            try
            {
                if (tracks is not null)
                {
                    var updates = new List<WriteModel<Track>>();
                    foreach (var doc in tracks)
                    {
                        FilterDefinition<Track> filter = Builders<Track>.Filter.Where(x => x.Id == doc.Id);
                        FieldDefinition<Track> field = "id";
                        UpdateDefinition<Track> update = Builders<Track>.Update.AddToSet(field, doc);
                        updates.Add(new UpdateOneModel<Track>(filter, update));
                    }
                    await tracksCollection.BulkWriteAsync(updates, new BulkWriteOptions() { IsOrdered = false });

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
        private async static Task<List<Track>?> FetchAllTracksFromApiAsync(string apiKey, ILogger logger= null)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string baseUrl = $"https://musicapi.audionetwork.com";
                    string url = baseUrl + "/tracks?limit=500";

                    httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

                    var response = await httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {                        
                        logger.LogError($"Non success code when fetching tracks from AudioNetwork API. Status Code: {response.StatusCode} Reason: {response.ReasonPhrase}");
                        return null;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var tracks = JsonConvert.DeserializeObject<List<Track>>(content);

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

