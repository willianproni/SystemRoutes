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

        public Person Get(string name) =>
            _person.Find<Person>(person => person.Name == name).FirstOrDefault();

        public async Task<Person> Create(Person newPerson)
        {
            var seachTeam = await SeachApi.SeachTeamNameInApi(newPerson.Team.NameTeam);

            newPerson.Team = seachTeam;
            _person.InsertOne(newPerson);
            return newPerson;
        }

        public void Update(string name, Person updatePerson) =>
            _person.ReplaceOne(person => person.Name == name, updatePerson);

        public void Remove(string name) =>
            _person.DeleteOne(person => person.Name == name);
    }
}
