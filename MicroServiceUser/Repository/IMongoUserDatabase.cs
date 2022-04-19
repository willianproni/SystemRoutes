namespace MicroServiceUser.Repository
{
    public interface IMongoUserDatabase
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
