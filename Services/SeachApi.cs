using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Services
{
    public class SeachApi
    {
        static readonly HttpClient client = new HttpClient();

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
    }
}
