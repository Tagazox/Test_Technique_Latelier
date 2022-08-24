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
        string _filePath;

        private readonly ILogger<PlayersStatisticsController> _logger;

        public PlayersStatisticsController(ILogger<PlayersStatisticsController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _filePath = environment.IsDevelopment() ? $"{Path.GetDirectoryName(Environment.ProcessPath)}\\data.json" : "./data.json";
        }

        [HttpGet(Name = "GetPlayersStatistics")]
        public PlayersStatistics Get()
        {
            DataAcessLayer dal = new DataAcessLayer(_filePath);
            PlayersStatisticsQueryProcessor queryProcessor = new PlayersStatisticsQueryProcessor(dal);
            return queryProcessor.GetPlayersStatistics();
        }

    }
}