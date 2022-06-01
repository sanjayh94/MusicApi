using MusicApi.Models;

namespace MusicApi.Interfaces
{
    /// <summary>
    /// Interface for fetching tracks and searching Tracks. Used as an abstraction for TrackService as a means to use Dependency Injection.
    /// </summary>
    public interface ITracksService
    {
        Task<List<TrackDTO>> GetAsync();
        Task<TrackDTO?> GetAsync(long id);

        Task<List<TrackDTO>> ListByWordAsync(string word);
        Task<long> GetTrackCountByWord(string word);
    }
}
