using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public abstract class DependencyInfo: MongoBase
    {
        [BsonElement("dependencyId")]
        public string DependencyId { get; set; }

        [BsonElement("version")]
        public VersionInfo Version { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("authors")]
        public List<string> Authors { get; set; }

        [BsonElement("owners")]
        public List<string> Owners { get; set; }

        [BsonElement("licenseUrl")]
        public string LicenseUrl { get; set; }

        [BsonElement("projectUrl")]
        public string ProjectUrl { get; set; }

        [BsonElement("iconUrl")]
        public string IconUrl { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }

        [BsonElement("dependencies")]
        public List<DependencyInfo> Dependencies { get; } = new List<DependencyInfo>();
    }
}