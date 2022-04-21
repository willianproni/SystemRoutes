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
        public static List<string> routes = new();
        public static readonly List<string> headers = new();
        public static string service;
        public static string city;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeamOperationCity()
        {
            service = Request.Form["serviceName"].ToString();
            city = Request.Form["cityName"].ToString();

            return RedirectToAction(nameof(SelectHeader));
        }

        public async Task<IActionResult> SelectHeader()
        {
            var teams = await SeachApi.SeachTeamCityIdInApiAsync(city);      

            ViewBag.TeamsAvailable = teams;

            ViewBag.retornoReadFile = routes;
            return View();
        }

        public async Task<IActionResult> SelectServiceAndCity(IFormFile file)
        {
            var localizarcity = SeachApi.GetAllCityInApi();
            ViewBag.AllCity = await localizarcity;

            var retorno = ReadFileExcel.ReadFile(file);
            routes = retorno;
            

            return View();
        }
    }
}
