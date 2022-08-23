using NetCoreTests.Data.Model;

namespace NetCoreTests.Queries
{
    public interface IPlayersStatisticsQueryProcessor
    {
        PlayersStatistics GetPlayersStatistics();
    }
}