using MongoDB.Bson;

namespace Arnis.API.Models
{
    public class MongoBase
    {
        public ObjectId Id { get; set; }
    }
}