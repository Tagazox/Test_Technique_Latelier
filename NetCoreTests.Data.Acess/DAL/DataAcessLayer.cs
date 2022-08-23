using NetCoreTests.API.Common.Exceptions;
using Newtonsoft.Json;

namespace NetCoreTests.Data.Acess.DAL
{
    public class DataAcessLayer : IDataAcessLayer
    {
        string _filePath { get; set; } 
        Newtonsoft.Json.Linq.JArray ParsedData { get; set; }
        public DataAcessLayer(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("Json file can't be found, can't retreive datas");
            try
            {
                ParsedData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(_filePath)).players;
            }
            catch (Exception e)
            {
                throw new NonConformFileException("Can't parse the JSON file");
            }
            if (ParsedData == null || !ParsedData.HasValues)
                    throw new NonConformFileException("The JSON file is empty");
           
        }
        public IQueryable<T> Query<T>()
            where T : class
        {
            return ParsedData.ToObject<IList<T>>().AsQueryable();
        }

    }
}
