namespace MicroServiceUser.Repository
{
    public class MongoUserDatabase : IMongoUserDatabase
    {
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
