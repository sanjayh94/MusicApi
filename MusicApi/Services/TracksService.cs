using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MusicApi.Interfaces;
using MusicApi.Models;

namespace MusicApi.Services
{
    /// <summary>
    /// Class which provides methods to retrieve Track info. Implements ITracksService interface
    /// </summary>
    public class TracksService: ITracksService
    {        
        #region Declarations
        private readonly IMongoCollection<Track> _tracksCollection;
        private readonly ILogger<TracksService> _logger;
        #endregion

        public TracksService(
            IOptions<MusicTracksDatabaseSettings> musicTracksDatabaseSettings, ILogger<TracksService> logger)
        {
            // Fetching DB Connection settings from Services DI Container
            // and Initialising MongoClient to interact with the DB Collection.
            var mongoClient = new MongoClient(
                musicTracksDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                musicTracksDatabaseSettings.Value.DatabaseName);

            _tracksCollection = mongoDatabase.GetCollection<Track>(
                musicTracksDatabaseSettings.Value.TracksCollectionName);

            _logger = logger; // Set up logger
        }

        /// <summary>
        /// Fetches all tracks from the DB
        /// </summary>
        /// <returns>List of tracks (DTO)</returns>
        public async Task<List<TrackDTO>> GetAsync()
        {
            var tracks = await _tracksCollection.Find(_ => true).ToListAsync();
            var tracksDto = tracks.ConvertAll(x => (TrackDTO)x);
            return tracksDto;
        }

        /// <summary>
        /// Fetches a single track from the DB for a given Track Id
        /// </summary>
        /// <param name="id">track id</param>
        /// <returns>Track (DTO)</returns>
        public async Task<TrackDTO?> GetAsync(long id)
        {
            var track = new TrackDTO();
            track = await _tracksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return track;
        }

        /// <summary>
        /// For a given word, fetches a list of tracks that has that word in its title from the DB and returns the count
        /// </summary>
        /// <param name="word">word to search titles</param>
        /// <returns>Count of tracks that matches the criteria</returns>
        public async Task<long> GetTrackCountByWord(string word)
        {
            FilterDefinition<Track> filter = Builders<Track>.Filter.Where(s => s.Title!.ToLower().Contains(word)); //.ToLower() filter enables searches to be case insensitive.
            var count = await _tracksCollection.CountDocumentsAsync(filter); // returns the count of documents returned that matches the filter

            return count;
        }

        /// <summary>
        /// For a given word, fetches a list of tracks that has that word in its title from the DB
        /// </summary>
        /// <param name="word">word to search titles</param>
        /// <returns>List of tracks that matches the criteria</returns>
        public async Task<List<TrackDTO>> ListByWordAsync(string word)
        {
            FilterDefinition<Track> filter = Builders<Track>.Filter.Where(s => s.Title!.ToLower().Contains(word));
            List<Track> results = await _tracksCollection.Find(filter).ToListAsync();

            var resultsDto = results.ConvertAll(x => (TrackDTO)x);
            return resultsDto;
        }
    }
}
