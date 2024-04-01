using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WordleOnlineServer.Models.MsSqlModels;

namespace WordleOnlineServer.Models.MongoModels
{
    public class Match
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MatchIdentifier { get; set; }
        public AppUser UserSender { get; set; }
        public AppUser UserReceiver { get; set; }
        public int LetterCount { get; set; }
        public string User1Letter { get; set; }
        public string User2Letter { get; set;}
        public bool Status { get; set; }

    }
}
