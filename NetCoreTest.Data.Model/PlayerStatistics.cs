using NetCoreTests.API.Common.Exceptions;

namespace NetCoreTests.Data.Model
{
    public class PlayersStatistics
    {
        IEnumerable<Player> _players { get; set; }
        public Country CountryWitchHasTheBestRatio { get; internal set; }
        public int MeanBmiOfThePlayers { get; internal set; }
        public double MedianHeightOfThePlayers { get; internal set; }
        public PlayersStatistics(IEnumerable<Player> players)
        {
            try
            {
                _players = players;
                List<Country> countryList = _players.Select(c => c.country).DistinctBy(c => c.code).ToList();
                int _playersCount = _players.Count();
                MedianHeightOfThePlayers = _playersCount % 2 == 0
                    ? _players.Select(x => x.data.height).OrderBy(x => x).Skip((_playersCount / 2) - 1).Take(2).Average()
                    : _players.Select(x => x.data.height).OrderBy(x => x).ElementAt(_playersCount / 2);

                MeanBmiOfThePlayers = (int)Math.Round(
                        _players.Select(d => (d.data.weight / 1000) / Math.Pow(d.data.height / 100, 2)).Average()
                        , MidpointRounding.AwayFromZero);

                CountryWitchHasTheBestRatio = countryList.First(c => c.code == _players.GroupBy(p => p.country.code)
                      .Select(d => new { Score =((float) d.Sum(s => s.data.last.Sum()) / (float)d.Sum(s => s.data.last.Count())), Country = d.Key })
                      .OrderByDescending(d => d.Score)
                      .Select(c => c.Country).First());
            }
            catch (Exception)
            {
                throw new IncompleteDataException("Not enought data to calculate the statistics");
            }

        }
    }
}