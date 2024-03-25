namespace WordleOnlineServer.Models.MongoModels
{
    public class Match
    {
        public User User1 { get; set; }
        public User User2 { get; set; }
        public int LetterCount { get; set; }
        public string User1Letter { get; set; }
        public string User2Letter { get; set;}

    }
}
