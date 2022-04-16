namespace MicroServiceRotas.Repository
{
    public interface IMongoDbDatabase
    {
        string MongoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
