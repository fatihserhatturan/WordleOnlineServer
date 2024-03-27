using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WordleOnlineServer.Models.MongoModels
{
    public class EnableUsers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<User> Users { get; set; }
    }
}
