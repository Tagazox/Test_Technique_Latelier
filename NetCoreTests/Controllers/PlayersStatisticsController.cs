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
        public string FilePath = $"{Path.GetDirectoryName(Environment.ProcessPath)}\\data.json";

        private readonly ILogger<PlayersStatisticsController> _logger;

        public PlayersStatisticsController(ILogger<PlayersStatisticsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPlayersStatistics")]
        public PlayersStatistics Get()
        {
            DataAcessLayer dal = new DataAcessLayer(FilePath);
            PlayersStatisticsQueryProcessor queryProcessor = new PlayersStatisticsQueryProcessor(dal);
            return queryProcessor.GetPlayersStatistics();
        }

    }
}