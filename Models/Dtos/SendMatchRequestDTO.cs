using WordleOnlineServer.Models.MsSqlModels;

namespace WordleOnlineServer.Models.Dtos
{
    public class SendMatchRequestDTO
    {
        public string Sender {  get; set; }
        public string Receiver {  get; set; }
    }
}
