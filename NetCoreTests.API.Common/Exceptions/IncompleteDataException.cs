namespace NetCoreTests.API.Common.Exceptions
{
    public class IncompleteDataException : Exception
    {
        public IncompleteDataException(string message) : base(message)
        {
        }
    }
}
