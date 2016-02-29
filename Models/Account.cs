using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.API.Models
{
    public class Account : MongoBase
    {
        [BsonElement("apiKey")]
        public string ApiKey { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }
    }
}