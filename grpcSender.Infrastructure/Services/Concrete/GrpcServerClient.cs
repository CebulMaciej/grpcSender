using System.Threading.Tasks;
using grpcSender.Infrastructure.Services.Abstract;

namespace grpcSender.Infrastructure.Services.Concrete
{
    public class GrpcServerClient : IGrpcServerClient
    {
        private readonly Sender.SenderClient _client;

        public GrpcServerClient(Sender.SenderClient client)
        {
            _client = client;
        }

        public async Task<string> Send(string data)
        {
            SendReply z = await _client.SendAsync(new SendRequest
            {
                Data = data
            });

            return z.Data;
        }
    }
}