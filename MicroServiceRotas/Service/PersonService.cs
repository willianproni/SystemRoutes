using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServiceRotas.Repository;
using Model.MongoDb;
using MongoDB.Driver;
using Services;

namespace MicroServiceRotas.Service
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _person;

        public PersonService(IMongoDbDatabase settings)
        {
            var person = new MongoClient(settings.ConnectionString);
            var database = person.GetDatabase(settings.DatabaseName);
            _person = database.GetCollection<Person>(settings.MongoCollectionName);
        }

        public List<Person> Get() =>
            _person.Find(person => true).ToList();

        public Person GetId(string id) =>
            _person.Find(person => person.Id == id).FirstOrDefault();

        public Person GetName(string name) =>
            _person.Find<Person>(person => person.Name == name).FirstOrDefault();

        public Person Create(Person newPerson)
        {
            _person.InsertOne(newPerson);
            return newPerson;
        }
            
        public void Update(string id, Person updatePerson) =>
            _person.ReplaceOne(person => person.Id == id, updatePerson);

        public void Remove(string id) =>
            _person.DeleteOne(person => person.Id == id);
    }
}
