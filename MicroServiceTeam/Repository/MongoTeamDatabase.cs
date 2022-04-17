namespace MicroServiceTeam.Repository
{
    public class MongoTeamDatabase : IMongoTeamDatabase
    {
        public string TeamCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
