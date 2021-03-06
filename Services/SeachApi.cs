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
                HttpResponseMessage response = await client.GetAsync("https://localhost:44365/api/City");
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

        public static async Task<List<Person>> GetAllPersonStatusTrue()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44302/status");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var personJson = JsonConvert.DeserializeObject<List<Person>>(responseBody);
                return personJson;
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

        public static async Task<Person> SeachPersonIdInApiAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44302/api/Person/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<Person>(responseBody);
                return PersonJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<City> SeachCityIdInApiAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44365/api/City/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var CityJson = JsonConvert.DeserializeObject<City>(responseBody);
                return CityJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Team> SeachTeamIdInApiAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44345/api/Team/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var TeamJson = JsonConvert.DeserializeObject<Team>(responseBody);
                return TeamJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<List<Team>> SeachTeamCityIdInApiAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44345/api/Team/cidade/team/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var TeamJson = JsonConvert.DeserializeObject<List<Team>>(responseBody);
                return TeamJson;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetName

        public static async Task<City> SeachCityNameInApi(string nameCity)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44365/api/City/cidade/" + nameCity);
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
                HttpResponseMessage response = await client.GetAsync("https://localhost:44345/api/Team/time/" + nameTeam);
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

        public static async Task<Person> SeachPersonNameInApiAsync(string namePerson)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44302/api/Person/nome/" + namePerson);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<Person>(responseBody);
                return PersonJson;
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
            client.PostAsJsonAsync("https://localhost:44365/api/City", newCity);
        }

        public static void PostTeam(Team newTeam)
        {
            client.PostAsJsonAsync("https://localhost:44345/api/Team", newTeam);
        }

        public static void PostPerson(Person newPerson)
        {
            client.PostAsJsonAsync("https://localhost:44302/api/Person", newPerson);
        }

        #endregion

        #region PutApi

        public static void UpdateTeam(string id, Team updateTeam)
        {
            client.PutAsJsonAsync("https://localhost:44345/api/Team/" + id, updateTeam);
        }

        public static void UpdatePerson(string id, Person updatePerson)
        {
            client.PutAsJsonAsync("https://localhost:44302/api/Person/" + id, updatePerson);
        }

        public static void UpdateCity(string id, City updateCity)
        {
            client.PutAsJsonAsync("https://localhost:44365/api/City/" + id, updateCity);
        }

        public static void UpdateTeamInsert(string id, Person updatePerson)
        {
            client.PutAsJsonAsync("https://localhost:44345/api/Team/insert/" + id, updatePerson);
        }

        public static void UpdateTeamRemove(string id, Person updatePerson)
        {
            client.PutAsJsonAsync("https://localhost:44345/api/Team/remove/" + id, updatePerson);
        }

        #endregion

        #region GetDelete

        public static void RemoveTeam(string id)
        {
            client.DeleteAsync("https://localhost:44345/api/Team/" + id);
        }

        public static void RemovePerson(string id)
        {
            client.DeleteAsync("https://localhost:44302/api/Person/" + id);
        }

        public static void RemoveCity(string id)
        {
            client.DeleteAsync("https://localhost:44365/api/City/" + id);
        }

        #endregion

        public static async Task UpdatePersonActive(string id)
        {
           var response = await client.PutAsync($"https://localhost:44302/api/Person/status/" + id, null);
        }

    }
}
