using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public class Project: MongoBase
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("dependencies")]
        public List<Dependency> Dependencies { get; set; } = new List<Dependency>();
    }
}