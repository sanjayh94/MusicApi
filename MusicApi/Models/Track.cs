using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MusicApi.Models
{
    public class Track
    {
        // Required for mapping the Common Language Runtime (CLR) object to the MongoDB collection.
        // Annotated with [BsonId] to make this property the document's primary key.
        // Annotated with [BsonRepresentation(BsonType.ObjectId)] to allow passing the parameter as type string instead of an ObjectId structure. Mongo handles the conversion from string to ObjectId.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [JsonProperty("id")]
        public long TrackId { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("duration")]
        public long? Duration { get; set; }

        [JsonProperty("endKey")]
        public string? EndKey { get; set; }

        [JsonProperty("metre")]
        public string? Metre { get; set; }

        [JsonProperty("startKey")]
        public string? StartKey { get; set; }

        [JsonProperty("endBpm")]
        public long EndBpm { get; set; }

        [JsonProperty("startBpm")]
        public long StartBpm { get; set; }

        [JsonProperty("tempo")]
        public string? Tempo { get; set; }

        [JsonProperty("isArrangement")]
        public bool IsArrangement { get; set; }       

        [JsonProperty("trackFact")]
        public string? TrackFact { get; set; }

        [JsonProperty("albumTrackNumber")]
        public long AlbumTrackNumber { get; set; }

        [JsonProperty("mix")]
        public long Mix { get; set; }

        [JsonProperty("parentId")]
        public long ParentId { get; set; }

        [JsonProperty("keywords")]
        public string? Keywords { get; set; }

        [JsonProperty("priorityOrder")]
        public long PriorityOrder { get; set; }

        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        [JsonProperty("mixType")]
        public string? MixType { get; set; }

        [JsonProperty("mixVariation")]
        public string? MixVariation { get; set; }

        [JsonProperty("releaseDate")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("releaseDateTimestamp")]
        public double ReleaseDateTimestamp { get; set; }

        [JsonProperty("isrc")]
        public string? Isrc { get; set; }

        [JsonProperty("album")]
        public Album? Album { get; set; }

        [JsonProperty("composers")]
        public List<Composer>? Composers { get; set; }

        [JsonProperty("publishers")]
        public List<Publisher>? Publishers { get; set; }

        [JsonProperty("mixCount")]
        public long MixCount { get; set; }

        [JsonProperty("relatedCount")]
        public long RelatedCount { get; set; }
       
        [JsonProperty("previewUrl")]
        public Uri? PreviewUrl { get; set; }
    }

    public class Album
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pillar")]
        public string? Pillar { get; set; }

        [JsonProperty("mainMixTrackCount")]
        public long MainMixTrackCount { get; set; }

        [JsonProperty("trackCount")]
        public long TrackCount { get; set; }
    }

    public class Composer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("collectionSociety")]
        public string? CollectionSociety { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("ipi")]
        public string? Ipi { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class Publisher
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("ipi")]
        public string? Ipi { get; set; }

        [JsonProperty("collectionSociety")]
        public string? CollectionSociety { get; set; }
    }
}
