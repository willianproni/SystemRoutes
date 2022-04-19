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

        public Team Get(string nameTeam) =>
            _team.Find<Team>(team => team.NameTeam == nameTeam).FirstOrDefault();

        public async Task<Team> Create(Team newTeam)
        {
            var personList = new List<Person>();

            foreach (var item in newTeam.Persons)
            {
                try
                {
                    Person verifyPerson = await SeachApi.SeachPersonNameInApiAsync(item.Name);
                    SeachApi.UpdatePerson(verifyPerson.Id, new Person()
                    {
                        Id = verifyPerson.Id,
                        Name = verifyPerson.Name,
                        Active = false
                    });
                    verifyPerson.Active = !verifyPerson.Active;
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

        public void Update(string nameTeam, Team updateTeam) =>
            _team.ReplaceOne(team => team.NameTeam == nameTeam, updateTeam);

        public async void Remove(string nameTeam)
        {
            var verifyTeam = await SeachApi.SeachTeamNameInApi(nameTeam);

            foreach (var item in verifyTeam.Persons)
            {
                SeachApi.UpdatePerson(item.Id, new Person()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Active = true
                });
            }

            _team.DeleteOne(team => team.NameTeam == nameTeam);
        }

    }
}
