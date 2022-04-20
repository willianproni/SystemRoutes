using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServiceTeam.Repository;
using Model.MongoDb;
using MongoDB.Driver;
using Services;

namespace MicroServiceTeam.Service
{
    public class TeamService
    {
        private readonly IMongoCollection<Team> _team;

        public TeamService(IMongoTeamDatabase settings)
        {
            var team = new MongoClient(settings.ConnectionString);
            var database = team.GetDatabase(settings.DatabaseName);
            _team = database.GetCollection<Team>(settings.TeamCollectionName);
        }

        public List<Team> Get() =>
            _team.Find(team => true).ToList();

        public Team GetId(string id) =>
            _team.Find(team => team.Id == id).FirstOrDefault();

        public Team Get(string nameTeam) =>
            _team.Find<Team>(team => team.NameTeam == nameTeam).FirstOrDefault();

        public async Task<Team> Create(Team newTeam)
        {
            var personList = new List<Person>();

            foreach (var item in newTeam.Persons)
            {
                try
                {
                    Person verifyPerson = await SeachApi.SeachPersonIdInApiAsync(item.Id);
                    SeachApi.UpdatePerson(verifyPerson.Id, new Person()
                    {
                        Id = verifyPerson.Id,
                        Name = verifyPerson.Name,
                        Active = false
                    });
                    verifyPerson.Active = false;
                    personList.Add(verifyPerson);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            var seachCityInApi = await SeachApi.SeachCityNameInApi(newTeam.City.NameCity);

            newTeam.Persons = personList;
            newTeam.City = seachCityInApi;
            _team.InsertOne(newTeam);
            return newTeam;
        }

        public void Update(string id, Team updateTeam) =>
            _team.ReplaceOne(team => team.Id == id, updateTeam);

        public async Task<Team> UpdateInsert(string id, Person updatePerson)
        {
            var seachTeam = await SeachApi.SeachTeamIdInApiAsync(id);

            var seachPerson = await SeachApi.SeachPersonIdInApiAsync(updatePerson.Id);

            if (seachTeam == null)
                return null;

            seachPerson.Active = false;

            var filter = Builders<Team>.Filter.Where(team => team.Id == id);
            var update = Builders<Team>.Update.Push("Persons", seachPerson);

            await SeachApi.UpdatePersonActive(seachPerson.Id);

            await _team.UpdateOneAsync(filter, update);

            return seachTeam;
        }

        public async Task<Team> UpdateRemove(string id, Person updatePerson)
        {
            var seachTeam = await SeachApi.SeachTeamIdInApiAsync(id);

            var seachPerson = await SeachApi.SeachPersonIdInApiAsync(updatePerson.Id);

            if (seachTeam == null)
                return null;

            var filter = Builders<Team>.Filter.Where(team => team.Id == id);
            var update = Builders<Team>.Update.Pull("Persons", seachPerson);

            await SeachApi.UpdatePersonActive(seachPerson.Id);

            await _team.UpdateOneAsync(filter, update);

            return seachTeam;
        }



        public async void Remove(string id)
        {
            var verifyTeam = await SeachApi.SeachTeamIdInApiAsync(id);

            foreach (var item in verifyTeam.Persons)
            {
                SeachApi.UpdatePerson(item.Id, new Person()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Active = true
                });
            }

            _team.DeleteOne(team => team.Id == id);
        }

    }
}
