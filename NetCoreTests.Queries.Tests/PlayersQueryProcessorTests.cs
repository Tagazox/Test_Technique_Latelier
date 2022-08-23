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
    public class PlayersQueryProcessorTests
    {
        Mock<IDataAcessLayer> _dal;
        List<Player> _playersList;
        IPlayersQueryProcessor _queryProceessor;
        Random _rand;
        public PlayersQueryProcessorTests()
        {
            _rand = new Random();
            _dal = new Mock<IDataAcessLayer>();

            _playersList = new List<Player>();
            _dal.Setup(x => x.Query<Player>()).Returns(_playersList.AsQueryable());

            _queryProceessor = new PlayersQueryProcessor(_dal.Object);
        }
        [Fact]
        public void GetShouldReturnAllPlayers()
        {
            _playersList.Add(new Player() { id = _rand.Next() });
            _playersList.Add(new Player() { id = _rand.Next() });
            _playersList.Add(new Player() { id = _rand.Next() });

            _queryProceessor.GetOrderByRank().Count().Should().Be(3);
        }
        [Fact]
        public void GetShouldReturnById()
        {
            int IdThatShouldBeFound = _rand.Next();

            _playersList.Add(new Player() { id = IdThatShouldBeFound });
            _playersList.Add(new Player() { id = _rand.Next() });
            _playersList.Add(new Player() { id = _rand.Next() });

            _queryProceessor.GetById(IdThatShouldBeFound).id.Should().Be(IdThatShouldBeFound);
        }
        [Fact]
        public void GetShouldThrowExceptionIfItemIsNotFoundById()
        {
            _playersList.Add(new Player() { id = _rand.Next() });
            _playersList.Add(new Player() { id = _rand.Next() });
            _playersList.Add(new Player() { id = _rand.Next() });

            Action get = () =>
            {
                _queryProceessor.GetById(_rand.Next());
            };
            get.Should().Throw<NotFoundException>();
        }
    }
}