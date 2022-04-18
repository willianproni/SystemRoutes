using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.MongoDb;
using Newtonsoft.Json;

namespace Services
{
    public class SeachApi 
    {
        static readonly HttpClient client = new HttpClient();

        #region GetAllApi

        public static async Task<List<City>> GetAllCityInApi()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44394/api/City");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cityJson = JsonConvert.DeserializeObject<List<City>>(responseBody);
                return cityJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<List<Person>> GetAllPeopleInApi()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44302/api/Person");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Person>>(responseBody);
                return PersonJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<List<Team>> GetAllTeamInApi()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44345/api/Team");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Team>>(responseBody);
                return PersonJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GetId

        public static async Task<City> SeachCityNameInApi(string nameCity)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44394/api/City/" + nameCity);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cityJson = JsonConvert.DeserializeObject<City>(responseBody);
                return cityJson;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<Team> SeachTeamNameInApi(string nameTeam)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44345/api/Team/" + nameTeam);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var teamJson = JsonConvert.DeserializeObject<Team>(responseBody);
                return teamJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region PostApi

        public static void PostCity(City newCity)
        {
            client.PostAsJsonAsync("https://localhost:44394/api/City", newCity);
        }

        public static void PostTeam(Team newTeam)
        {
            client.PostAsJsonAsync("https://localhost:44345/api/Team", newTeam);
        }

        #endregion

        #region PutApi

        public static void UpdateTeam(string id, Team updateTeam)
        {
            client.PutAsJsonAsync("https://localhost:44345/api/Team/" + id, updateTeam);
        }

        #endregion

        #region GetDelete

        public static void RemoveTeam(string name)
        {
            client.DeleteAsync("https://localhost:44345/api/Team/" + name);
        }

        #endregion

    }
}
