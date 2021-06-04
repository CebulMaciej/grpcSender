using System;
using System.Threading.Tasks;
using grpcSender.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Open.Serialization.Json.Newtonsoft;

namespace grpcSender
{
    public static class Program
    {
        public static Task Main(string[] args)
        =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddControllers().AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });

                    services.TryAddSingleton(new JsonSerializerFactory().GetSerializer());


                    services
                        .AddInfrastructure()
                        ;
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    services.AddGrpc();
                }).Configure(app =>
                {
                    app
                        .UseRouting()
                        .UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
                        .UseEndpoints(e => e.MapControllers());
                })
                .Build()
                .RunAsync();
            
    }
}