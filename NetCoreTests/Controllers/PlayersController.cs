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
        private PlayersQueryProcessor _queryProcessor;
        private readonly ILogger<PlayersController> _logger;
        public PlayersController(ILogger<PlayersController> logger, IWebHostEnvironment environment, IDataAcessLayer _dalService)
        {
            _logger = logger;
            _queryProcessor = new PlayersQueryProcessor(_dalService);
        }

        [HttpGet(Name = "GetPlayersOrderByRank")]
        public IEnumerable<Player> Get()
        {
            return _queryProcessor.GetOrderByRank().ToList();
        }

        [HttpGet("{id}", Name = "GetPlayerById")]
        public Player Get(int id)
        {
            return _queryProcessor.GetById(id);
        }

    }
}