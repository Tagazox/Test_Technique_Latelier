
namespace NetCoreTests.Data.Acess.DAL
{
    public interface IDataAcessLayer
    {
        IQueryable<T> Query<T>() where T : class;
    }
}