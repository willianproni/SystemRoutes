namespace MicroServiceTeam.Repository
{
    public interface IMongoTeamDatabase
    {
        string TeamCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
