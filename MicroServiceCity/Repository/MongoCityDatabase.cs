namespace MicroServiceCity.Repository
{
    public class MongoCityDatabase : IMongoCityDatabase
    {
        public string CityCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
