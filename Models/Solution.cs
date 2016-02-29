using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public class Solution : MongoBase
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("projects")]
        public List<Project> Projects { get; set; } = new List<Project>();

        [BsonElement("dependencies")]
        public List<Dependency> Dependencies { get; set; } = new List<Dependency>();
    }
}