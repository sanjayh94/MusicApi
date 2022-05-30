using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MusicApi.Models
{
    [BsonIgnoreExtraElements]    
    public class Track
    {
        // Required for mapping the Common Language Runtime (CLR) object to the MongoDB collection.
        // Annotated with [BsonId] to make this property the document's primary key.
        // Annotated with [BsonRepresentation(BsonType.ObjectId)] to allow passing the parameter as type string instead of an ObjectId structure. Mongo handles the conversion from string to ObjectId.
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string? Id { get; set; }

        [BsonElement("id")]
        [BsonId]
        public long Id { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("duration")]
        public long? Duration { get; set; }

        [BsonElement("endKey")]
        public string? EndKey { get; set; }

        [BsonElement("metre")]
        public string? Metre { get; set; }

        [BsonElement("startKey")]
        public string? StartKey { get; set; }

        [BsonElement("endBpm")]
        public long? EndBpm { get; set; }

        [BsonElement("startBpm")]
        public long? StartBpm { get; set; }

        [BsonElement("tempo")]
        public string? Tempo { get; set; }

        [BsonElement("isArrangement")]
        public bool? IsArrangement { get; set; }       

        [BsonElement("trackFact")]
        public string? TrackFact { get; set; }

        [BsonElement("albumTrackNumber")]
        public long? AlbumTrackNumber { get; set; }

        [BsonElement("mix")]
        public long? Mix { get; set; }

        [BsonElement("parentId")]
        public long? ParentId { get; set; }

        [BsonElement("keywords")]
        public string? Keywords { get; set; }

        [BsonElement("priorityOrder")]
        public long? PriorityOrder { get; set; }

        [BsonElement("explicit")]
        public bool? Explicit { get; set; }

        [BsonElement("mixType")]
        public string? MixType { get; set; }

        [BsonElement("mixVariation")]
        public string? MixVariation { get; set; }

        [BsonElement("releaseDate")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? ReleaseDate { get; set; }

        [BsonElement("releaseDateTimestamp")]
        public double? ReleaseDateTimestamp { get; set; }

        [BsonElement("isrc")]
        public string? Isrc { get; set; }

        [BsonElement("album")]
        public Album? Album { get; set; }

        [BsonElement("composers")]
        public List<Composer>? Composers { get; set; }

        [BsonElement("publishers")]
        public List<Publisher>? Publishers { get; set; }

        [BsonElement("mixCount")]
        public long? MixCount { get; set; }

        [BsonElement("relatedCount")]
        public long? RelatedCount { get; set; }
       
        [BsonElement("previewUrl")]
        public Uri? PreviewUrl { get; set; }
    }

    public class Album
    {
        [BsonElement("number")]
        public long? Number { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("pillar")]
        public string? Pillar { get; set; }

        [BsonElement("mainMixTrackCount")]
        public long? MainMixTrackCount { get; set; }

        [BsonElement("trackCount")]
        public long? TrackCount { get; set; }
    }
 
    public class Composer
    {
        [BsonElement("id")]
        public long? ComposerId { get; set; }

        [BsonElement("collectionSociety")]
        public string? CollectionSociety { get; set; }

        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("ipi")]
        public string? Ipi { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }
    }
    
    public class Publisher
    {
        [BsonElement("id")]
        public long? PublisherId { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("ipi")]
        public string? Ipi { get; set; }

        [BsonElement("collectionSociety")]
        public string? CollectionSociety { get; set; }
    }
}
