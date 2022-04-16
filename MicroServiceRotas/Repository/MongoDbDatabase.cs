namespace MicroServiceRotas.Repository
{
    public class MongoDbDatabase : IMongoDbDatabase
    {
        public string MongoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
