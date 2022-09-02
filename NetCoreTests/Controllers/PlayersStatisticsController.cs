using Microsoft.AspNetCore.Mvc;
using NetCoreTests.Data.Acess.DAL;
using NetCoreTests.Data.Model;
using NetCoreTests.Queries;

namespace NetCoreTests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersStatisticsController : ControllerBase
    {

        private readonly ILogger<PlayersStatisticsController> _logger;
        private PlayersStatisticsQueryProcessor queryProcessor;

        public PlayersStatisticsController(ILogger<PlayersStatisticsController> logger, IWebHostEnvironment environment, IDataAcessLayer _dalService)
        {
            _logger = logger;
            queryProcessor = new PlayersStatisticsQueryProcessor(_dalService);
        }

        [HttpGet(Name = "GetPlayersStatistics")]
        public PlayersStatistics Get()
        {
            return queryProcessor.GetPlayersStatistics();
        }

    }
}