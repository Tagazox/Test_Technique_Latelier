using NetCoreTests.API.Common.Exceptions;

namespace NetCoreTests.Data.Model
{
    public class PlayersStatistics
    {
        public Country CountryWitchHasTheBestRatio { get; internal set; }
        public int MeanBmiOfThePlayers { get; internal set; }
        public double MedianHeightOfThePlayers { get; internal set; }
        public PlayersStatistics(IEnumerable<Player> players)
        {
            try
            {
                List<Country> countryList = players.Select(c => c.country).DistinctBy(c => c.code).ToList();
                int playersCount = players.Count();
                MedianHeightOfThePlayers = playersCount % 2 == 0
                    ? players.Select(x => x.data.height).OrderBy(x => x).Skip((playersCount / 2) - 1).Take(2).Average()
                    : players.Select(x => x.data.height).OrderBy(x => x).ElementAt(playersCount / 2);

                MeanBmiOfThePlayers = (int)Math.Round(
                        players.Select(d => (d.data.weight / 1000) / Math.Pow(d.data.height / 100, 2)).Average()
                        , MidpointRounding.AwayFromZero);

                CountryWitchHasTheBestRatio = countryList.First(c => c.code == players.GroupBy(p => p.country.code)
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