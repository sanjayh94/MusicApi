using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MusicApi.Interfaces;
using MusicApi.Models;

namespace MusicApi.Services
{
    public class TracksService: ITracksService
    {        
        #region Declarations
        private readonly IMongoCollection<Track> _tracksCollection;
        private readonly ILogger<TracksService> _logger;
        #endregion

        public TracksService(
            IOptions<MusicTracksDatabaseSettings> musicTracksDatabaseSettings, ILogger<TracksService> logger)
        {
            var mongoClient = new MongoClient(
                musicTracksDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                musicTracksDatabaseSettings.Value.DatabaseName);

            _tracksCollection = mongoDatabase.GetCollection<Track>(
                musicTracksDatabaseSettings.Value.TracksCollectionName);

            _logger = logger;
        }

        public async Task<List<Track>> GetAsync()
        {
            return await _tracksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Track?> GetAsync(long id)
        {
            return await _tracksCollection.Find(x => x.TrackId == id).FirstOrDefaultAsync();
        }

        public async Task<long> GetTrackCountByWord(string word)
        {
            FilterDefinition<Track> filter = Builders<Track>.Filter.Where(s => s.Title!.ToLower().Contains(word));
            var count = await _tracksCollection.CountDocumentsAsync(filter);

            return count;
        }

        public async Task<List<Track>> ListByWordAsync(string word)
        {
            FilterDefinition<Track> filter = Builders<Track>.Filter.Where(s => s.Title!.ToLower().Contains(word));
            List<Track> results = await _tracksCollection.Find(filter).ToListAsync();

            return results;
        }
    }
}
