using System.Threading.Tasks;
using grpcSender.Infrastructure.Services.Abstract;
using grpcSender.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grpcSender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendController : ControllerBase
    {
        private readonly ILogger<SendController> _logger;
        private readonly IGrpcServerClient _client;
        public SendController(ILogger<SendController> logger, IGrpcServerClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public ActionResult Get()
        => Ok("To send message use httpPost");
        
        [HttpPost]
        public async Task<ActionResult> Send(SendRequestModel sendRequestModel)
        {
            _logger.Log(LogLevel.Information,$"Handled {sendRequestModel.Message}");
            await _client.Send(sendRequestModel.Message);
            return Ok();
        }
    }
}