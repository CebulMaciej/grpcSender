using System;
using grpcSender.Infrastructure.Services.Abstract;
using grpcSender.Infrastructure.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace grpcSender.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<IGrpcServerClient, GrpcServerClient>();
            
            serviceProvider.AddGrpcClient<Sender.SenderClient>(o =>
            {
                o.Address = new Uri("http://localhost:5011");
            });
            
            
            return serviceProvider;
        }
    }
}