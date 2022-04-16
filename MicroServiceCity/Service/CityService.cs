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

        public City Get(string nameCity) =>
            _city.Find<City>(city => city.NameCity == nameCity).FirstOrDefault();

        public City Create(City newCity)
        {
            _city.InsertOne(newCity);
            return newCity;
        }

        public void Update(string nameCity, City updateCity) =>
            _city.ReplaceOne(city => city.NameCity == nameCity, updateCity);

        public void Remove(string nameCity) =>
            _city.DeleteOne(city => city.NameCity == nameCity);
    }
}
