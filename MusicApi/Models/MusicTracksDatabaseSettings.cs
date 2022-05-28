namespace MusicApi.Models
{
    public class MusicTracksDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TracksCollectionName { get; set; } = null!;
    }
}
