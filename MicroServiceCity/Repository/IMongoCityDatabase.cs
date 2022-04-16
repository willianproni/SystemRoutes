namespace MicroServiceCity.Repository
{
    public interface IMongoCityDatabase
    {
        string CityCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
