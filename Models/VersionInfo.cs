using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public abstract class VersionInfo: MongoBase
    {
        [BsonElement("major")]
        public int Major { get; set; }

        [BsonElement("minor")]
        public int Minor { get; set; }

        [BsonElement("build")]
        public int Build { get; set; }

        [BsonElement("revision")]
        public int Revision { get; set; }
    }
}