using Moq;
using NetCoreTests.Data.Acess.DAL;
using NetCoreTests.Data.Model;
using NetCoreTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using NetCoreTests.API.Common.Exceptions;

namespace NetCoreTests.Queries.Tests
{
    public class PlayersStatisticsQueryProcessorTests
    {
        Mock<IDataAcessLayer> _dal;
        List<Player> _playersList;
        IPlayersStatisticsQueryProcessor _queryProceessor;
        Random _rand;

        public PlayersStatisticsQueryProcessorTests()
        {
            _rand = new Random();
            _dal = new Mock<IDataAcessLayer>();

            _playersList = new List<Player>();
            _dal.Setup(x => x.Query<Player>()).Returns(_playersList.AsQueryable());

            _queryProceessor = new PlayersStatisticsQueryProcessor(_dal.Object);
        }
        [Fact]
        public void GetShouldReturnPlayersStatistics()
        {

            _playersList.Add(new Player()
            {
                data = new Data.Model.Data()
                {
                    weight = 1000,
                    height = 100,
                    last = new int[] { 1, 1, 1, 1 }
                },
                country = new Country()
                {
                    code = "USA",
                    picture = ""
                },
            });
            _playersList.Add(new Player()
            {
                data = new Data.Model.Data()
                {
                    weight = 1000,
                    height = 100,
                    last = new int[] { 0, 0, 0, 0 }
                },
                country = new Country()
                {
                    code = "USA",
                    picture = ""
                },
            });
            _playersList.Add(new Player()
            {
                data = new Data.Model.Data()
                {
                    weight = 1000,
                    height = 100,
                    last = new int[] { 1, 1, 1, 1 }
                },
                country = new Country()
                {
                    code = "FRA",
                    picture = ""
                },
            });

            _queryProceessor.GetPlayersStatistics().MeanBmiOfThePlayers.Should().Be(1);
            _queryProceessor.GetPlayersStatistics().MedianHeightOfThePlayers.Should().Be(100);
            _queryProceessor.GetPlayersStatistics().CountryWitchHasTheBestRatio.code.Should().Be("FRA");
        }
        [Fact]
        public void GetShouldReturnThrowIncompleteDataException()
        {

            _playersList.Add(new Player()
            {
                data = new Data.Model.Data()
                {

                },
                country = new Country()
                {
                    code = "USA"
                },
            });
            _playersList.Add(new Player()
            {
                data = new Data.Model.Data()
                {
                    weight = 1000,
                    height = 100,
                    last = new int[] { 0, 0, 0, 0 }
                },
                country = new Country()
                {
                    code = "USA"
                },
            });
            _playersList.Add(new Player()
            {
                data = new Data.Model.Data()
                {
                    weight = 1000,
                    height = 100,
                    last = new int[] { 1, 1, 1, 1 }
                },
                country = new Country()
                {
                    code = "FRA"
                },
            });

            Action get = () =>
            {
                var BmiOfThePlayers = _queryProceessor.GetPlayersStatistics();
            };
            get.Should().Throw<IncompleteDataException>();
        }

    }
}