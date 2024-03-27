using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WordleOnlineServer.Models.MongoModels
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
