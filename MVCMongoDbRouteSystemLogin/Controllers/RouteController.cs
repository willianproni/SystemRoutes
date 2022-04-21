using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.MongoDb;
using Services;

namespace MVCMongoDbRouteSystemLogin.Controllers
{
    public class RouteController : Controller
    {
        public IActionResult Index(IFormFile file)
        {

            var retorno = ReadFileExcel.ReadFile(file);

            ViewBag.retornoReadFile = retorno;

            return View();
        }

        /*public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameTeam")] Team team)
        {
            List<Person> teamPersons = new List<Person>();

            var cityCheck = Request.Form["city"].FirstOrDefault();
            var seachCity = await SeachApi.SeachCityNameInApi(cityCheck);
            var personCheck = Request.Form["checkPeopleTeam"].ToList();

            if (personCheck.Count != 0)
            {
                foreach (var item in personCheck)
                {
                    var veridyPeople = SeachApi.SeachPersonIdInApiAsync(item);
                    teamPersons.Add(await veridyPeople);
                }
            }
            else
            {
                return BadRequest(new { message = "A equipe precisa de pelo menos 1 integrante" });
            }

            team.Persons = teamPersons;
            team.City = seachCity;
            SeachApi.PostTeam(team);
            return RedirectToAction(nameof(Index));
        }*/
    }
}
