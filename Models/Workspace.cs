using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public class Workspace: MongoBase
    {
        public ObjectId AccountId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("owners")]
        public List<string> Owners { get; set; }

        [BsonElement("solutions")]
        public List<Solution> Solutions { get; set; } = new List<Solution>();
    }
}