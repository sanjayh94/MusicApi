using Newtonsoft.Json;

namespace MusicApi.Models
{
    /// <summary>
    ///  Model Data class for Data Transfer Object. This is the class used to send final data to User.
    ///  DTO classes can be used to select which data to send to the User. Useful for concealing data not meant for the user.
    /// </summary>
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

        /// <summary>
        /// Method to do implicit object conversions from Track Class to TrackDTO Class
        /// </summary>        
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
