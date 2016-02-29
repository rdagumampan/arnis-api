using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public class Dependency: MongoBase
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }
    }
}