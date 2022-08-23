using NetCoreTests.Data.Acess.DAL;
using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NetCoreTests.API.Common.Exceptions;

namespace NetCoreTests.Data.Acess.Tests
{
    public class DataAcessLayerTests
    {
        [Fact]
        public void ShouldCreateTheDalFine()
        {
            string filePath = $"{Path.GetDirectoryName(Environment.ProcessPath)}\\RealJsonFile.json";
            Action Create = () =>
            {
                IDataAcessLayer DAL = new DataAcessLayer(filePath);
            };
            Create.Should().NotThrow<Exception>();
        }
        [Fact]
        public void ShouldThrowFileNotFoundException()
        {
            string filePath = $"{Path.GetDirectoryName(Environment.ProcessPath)}\\NotHereFile.json";
            Action Create = () =>
            {
                IDataAcessLayer DAL = new DataAcessLayer(filePath);
            };
            Create.Should().Throw<FileNotFoundException>();
        }
        [Fact]
        public void ShouldThrowNonConformFileExceptionWithCantParseMessage()
        {
            string filePath = $"{Path.GetDirectoryName(Environment.ProcessPath)}\\JsonFileWithErrors.json";
            Action Create = () =>
            {
                IDataAcessLayer DAL = new DataAcessLayer(filePath);
            };
            Create.Should().Throw<NonConformFileException>().WithMessage("Can't parse the JSON file");
        }
        [Fact]
        public void ShouldThrowNonConformFileException()
        {
            string filePath = $"{Path.GetDirectoryName(Environment.ProcessPath)}\\DumbJsonFile.json";
            Action Create = () =>
            {
                IDataAcessLayer DAL = new DataAcessLayer(filePath);
            };
            Create.Should().Throw<NonConformFileException>().WithMessage("The JSON file is empty");
        }
    }
}