namespace WordleOnlineServer.Models.Dtos
{
    public class AcceptMatchReturnDTO
    {
        public string MatchIdentifier { get; set; }
        public string UserSenderName { get; set; }
        public string UserReceiverName { get; set; }

        // Constructor
        public AcceptMatchReturnDTO(string matchIdentifier, string userSenderName, string userReceiverName)
        {
            MatchIdentifier = matchIdentifier;
            UserSenderName = userSenderName;
            UserReceiverName = userReceiverName;
        }
    }
}
