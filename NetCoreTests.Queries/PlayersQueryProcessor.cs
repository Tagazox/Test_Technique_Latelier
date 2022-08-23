using NetCoreTests.API.Common.Exceptions;
using NetCoreTests.Data.Acess.DAL;
using NetCoreTests.Data.Model;

namespace NetCoreTests.Queries
{
    public class PlayersQueryProcessor : IPlayersQueryProcessor
    {
        IDataAcessLayer _dal { get; set; }
        public PlayersQueryProcessor(IDataAcessLayer DAL)
        {
            _dal = DAL;
        }
        public IQueryable<Player> GetOrderByRank()
        {
            return _dal.Query<Player>().OrderBy(p => p.data.rank);
        }
        public Player GetById(int id)
        {
            Player player = _dal.Query<Player>().FirstOrDefault(p => p.id == id) ;
            if (player == null)
                throw new NotFoundException("This player doesn't exist");
            return player;
        }
    }
}
