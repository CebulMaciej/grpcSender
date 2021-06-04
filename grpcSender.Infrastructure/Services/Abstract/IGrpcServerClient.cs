using System.Threading.Tasks;

namespace grpcSender.Infrastructure.Services.Abstract
{
    public interface IGrpcServerClient
    {
        Task<string> Send(string data);
    }
}