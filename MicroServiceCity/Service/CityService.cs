using System.Collections.Generic;
using MicroServiceCity.Repository;
using Model;
using MongoDB.Driver;

namespace MicroServiceCity.Service
{
    public class CityService
    {
        private readonly IMongoCollection<City> _city;

        public CityService(IMongoCityDatabase settings)
        {
            var city = new MongoClient(settings.ConnectionString);
            var database = city.GetDatabase(settings.DatabaseName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }

        public List<City> Get() =>
            _city.Find(city => true).ToList();

        public City GetId(string id) =>
            _city.Find(city => city.Id == id).FirstOrDefault();

        public City Get(string nameCity) =>
            _city.Find<City>(city => city.NameCity == nameCity).FirstOrDefault();

        public City Create(City newCity)
        {
            _city.InsertOne(newCity);
            return newCity;
        }

        public void Update(string id, City updateCity) =>
            _city.ReplaceOne(city => city.Id == id, updateCity);

        public void Remove(string id) =>
            _city.DeleteOne(city => city.Id == id);
    }
}
