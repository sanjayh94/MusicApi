using MusicApi.Models;

namespace MusicApi.Interfaces
{
    public interface ITracksService
    {
        Task<List<TrackDTO>> GetAsync();
        Task<TrackDTO?> GetAsync(long id);

        Task<List<TrackDTO>> ListByWordAsync(string word);
        Task<long> GetTrackCountByWord(string word);
    }
}
