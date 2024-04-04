namespace WordleOnlineServer.Models.Dtos
{
    public class GetUserLetterDto
    {
        public string Username { get; set; }
        public string MatchIdentifier { get; set; }
        public string Letter { get; set; }
    }
}
