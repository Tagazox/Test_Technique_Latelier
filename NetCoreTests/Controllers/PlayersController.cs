using Microsoft.AspNetCore.Mvc;
using NetCoreTests.Data.Acess.DAL;
using NetCoreTests.Data.Model;
using NetCoreTests.Queries;

namespace NetCoreTests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        public string FilePath = $"{Path.GetDirectoryName(Environment.ProcessPath)}\\data.json";
        private readonly ILogger<PlayersController> _logger;
        public PlayersController(ILogger<PlayersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPlayersOrderByRank")]
        public IEnumerable<Player> Get()
        {
            DataAcessLayer dal = new DataAcessLayer(FilePath);
            PlayersQueryProcessor queryProcessor = new PlayersQueryProcessor(dal);
            return queryProcessor.GetOrderByRank().ToList();
        }

        [HttpGet("{id}", Name = "GetPlayerOrderById")]
        public Player Get(int id)
        {
            DataAcessLayer dal = new DataAcessLayer(FilePath);
            PlayersQueryProcessor queryProcessor = new PlayersQueryProcessor(dal);
            return queryProcessor.GetById(id);
        }
       
    }
}