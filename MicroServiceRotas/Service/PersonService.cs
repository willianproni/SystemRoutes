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

        public List<Person> GetStatus() =>
            _person.Find(person => person.Active == true).ToList();


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

        public Person UpdateActive(string id)
        {
            var seachPerson = GetId(id);

            if (seachPerson == null)
                return null;

            seachPerson.Active = !seachPerson.Active;

            _person.ReplaceOneAsync<Person>(person => person.Id == id, seachPerson);
            return seachPerson;
        }

        public void Remove(string id) =>
            _person.DeleteOne(person => person.Id == id);
    }
}
