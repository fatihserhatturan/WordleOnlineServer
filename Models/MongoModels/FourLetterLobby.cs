using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WordleOnlineServer.Models.MsSqlModels;

namespace WordleOnlineServer.Models.MongoModels
{
    public class FourLetterLobby
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public AppUser User { get; set; }

    }
}
