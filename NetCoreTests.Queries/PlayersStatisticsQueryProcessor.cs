using NetCoreTests.Data.Acess.DAL;
using NetCoreTests.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTests.Queries
{
    public class PlayersStatisticsQueryProcessor : IPlayersStatisticsQueryProcessor
    {
        IDataAcessLayer _dal { get; set; }
        public PlayersStatisticsQueryProcessor(IDataAcessLayer DAL)
        {
            _dal = DAL;
        }
        public PlayersStatistics GetPlayersStatistics()
        {
            IEnumerable<Player> playerDataSet = _dal.Query<Player>();   
            return new PlayersStatistics(playerDataSet);
        }
    }
}
