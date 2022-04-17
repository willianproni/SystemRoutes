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

        public async Task<Team> Create (Team newTeam)
        {
            var seachCityInApi = await SeachApi.SeachCityNameInApi(newTeam.City.NameCity);

            newTeam.City = seachCityInApi;
            _team.InsertOne(newTeam);
            return newTeam;
        }

        public void Update(string nameTeam, Team updateTeam) =>
            _team.ReplaceOne(team => team.NameTeam == nameTeam, updateTeam);

        public void Remove(string nameTeam) =>
            _team.DeleteOne(team => team.NameTeam == nameTeam);
    }
}
