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

        public async Task<List<TrackDTO>> GetAsync()
        {
            var tracks = await _tracksCollection.Find(_ => true).ToListAsync();
            var tracksDto = tracks.ConvertAll(x => (TrackDTO)x);
            return tracksDto;
        }

        public async Task<TrackDTO?> GetAsync(long id)
        {
            var track = new TrackDTO();
            track = await _tracksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return track;
        }

        public async Task<long> GetTrackCountByWord(string word)
        {
            FilterDefinition<Track> filter = Builders<Track>.Filter.Where(s => s.Title!.ToLower().Contains(word));
            var count = await _tracksCollection.CountDocumentsAsync(filter);

            return count;
        }

        public async Task<List<TrackDTO>> ListByWordAsync(string word)
        {
            FilterDefinition<Track> filter = Builders<Track>.Filter.Where(s => s.Title!.ToLower().Contains(word));
            List<Track> results = await _tracksCollection.Find(filter).ToListAsync();

            var resultsDto = results.ConvertAll(x => (TrackDTO)x);
            return resultsDto;
        }
    }
}
