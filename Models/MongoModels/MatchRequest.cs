using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WordleOnlineServer.Models.MsSqlModels;

namespace WordleOnlineServer.Models.MongoModels
{
    public class MatchRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public AppUser UserSender { get; set; }
        public AppUser UserReceiver { get; set; }
        public int LetterCount { get; set; }
        public bool Status { get; set; }
    }
}
