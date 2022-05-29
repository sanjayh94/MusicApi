using MusicApi.Models;

namespace MusicApi.Interfaces
{
    public interface ITracksService
    {
        Task<List<Track>> GetAsync();
        Task<Track?> GetAsync(long id);

        Task<List<Track>> ListByWordAsync(string word);
        Task<long> GetTrackCountByWord(string word);
    }
}
