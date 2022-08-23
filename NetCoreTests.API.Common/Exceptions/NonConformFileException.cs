
namespace NetCoreTests.API.Common.Exceptions
{
    public class NonConformFileException : Exception
    {
        public NonConformFileException(string message) : base(message)
        {
        }
    }
}
