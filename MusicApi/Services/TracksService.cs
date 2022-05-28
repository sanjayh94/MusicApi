using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicApi.Models;

namespace MusicApi.Services
{
    public class TracksService
    {
        #region Declarations
        private readonly IMongoCollection<Track> _tracksCollection;
        #endregion

        public TracksService(
            IOptions<MusicTracksDatabaseSettings> musicTracksDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                musicTracksDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                musicTracksDatabaseSettings.Value.DatabaseName);

            _tracksCollection = mongoDatabase.GetCollection<Track>(
                musicTracksDatabaseSettings.Value.TracksCollectionName);
        }

        public async Task<List<Track>> GetAsync() =>
        await _tracksCollection.Find(_ => true).ToListAsync();

        public async Task<Track?> GetAsync(long id) =>
            await _tracksCollection.Find(x => x.TrackId == id).FirstOrDefaultAsync();
    }
}
