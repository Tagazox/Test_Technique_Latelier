using NetCoreTests.Data.Model;

namespace NetCoreTests.Queries
{
    public interface IPlayersQueryProcessor
    {
        IQueryable<Player> GetOrderByRank();
        Player GetById(int id);
    }
}
