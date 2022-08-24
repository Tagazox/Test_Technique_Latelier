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
        string _filePath;
        private readonly ILogger<PlayersController> _logger;
        public PlayersController(ILogger<PlayersController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _filePath = environment.IsDevelopment() ? $"{Path.GetDirectoryName(Environment.ProcessPath)}\\data.json" : "./data.json";
        }

        [HttpGet(Name = "GetPlayersOrderByRank")]
        public IEnumerable<Player> Get()
        {
            DataAcessLayer dal = new DataAcessLayer(_filePath);
            PlayersQueryProcessor queryProcessor = new PlayersQueryProcessor(dal);
            return queryProcessor.GetOrderByRank().ToList();
        }

        [HttpGet("{id}", Name = "GetPlayerById")]
        public Player Get(int id)
        {
            DataAcessLayer dal = new DataAcessLayer(_filePath);
            PlayersQueryProcessor queryProcessor = new PlayersQueryProcessor(dal);
            return queryProcessor.GetById(id);
        }
       
    }
}