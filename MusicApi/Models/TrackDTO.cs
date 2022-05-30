using Newtonsoft.Json;

namespace MusicApi.Models
{
    public class TrackDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("keywords")]
        public string? Keywords { get; set; }

        [JsonProperty("explicit")]
        public bool? Explicit { get; set; }

        [JsonProperty("releaseDate")]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("album")]
        public Album? Album { get; set; }


        public static implicit operator TrackDTO(Track v)
        {
            var track = new TrackDTO
            {
                Id = v.Id,
                Description = v.Description,
                Title = v.Title,
                Keywords = v.Keywords,
                Explicit = v.Explicit,
                ReleaseDate = v.ReleaseDate,
                Album = v.Album
            };

            return track;
        }
    }  
}
