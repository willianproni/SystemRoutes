using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.MongoDb;
using OfficeOpenXml;
using Services;
using System.IO;

namespace MVCMongoDbRouteSystemLogin.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {
        private static IWebHostEnvironment _hostEnvironment;
        public static List<List<string>> routes = new();
        public static List<string> headers = new();
        public static IEnumerable<string> serviceList;
        public static string service;
        public static string city;
        public static string downloadFile;

        public RouteController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeamOperationCity(IFormFile file)
        {
            (headers, routes, serviceList) = ReadFileExcel.ReadFileInput(file);

            return RedirectToAction(nameof(SelectServiceAndCity));
        }

        public async Task<IActionResult> SelectServiceAndCity()
        {
            var localizarcity = SeachApi.GetAllCityInApi();
            ViewBag.AllCity = await localizarcity;
            ViewBag.AllService = serviceList;
            return View();
        }

        public async Task<IActionResult> SelectHeader()
        {
            city = Request.Form["cityName"].ToString();
            service = Request.Form["serviceName"].ToString();

            var team = await SeachApi.SeachTeamCityIdInApiAsync(city);

            ViewBag.TeamCity = team;

            if (team.Count == 0)
            {
                return BadRequest(new { message = "A cidade selecionada não tem Equipe!!" });
            }

            ViewBag.retornoReadFile = headers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenereatorDoc()
        {
            List<Team> teams = new();
            var teamSelect = Request.Form["checkTeamService"].ToList();
            var headerSelect = Request.Form["checkHeader"].ToList();

            foreach (var item in teamSelect)
            {
                var seachTeam = await SeachApi.SeachTeamIdInApiAsync(item);
                teams.Add(seachTeam);
            }

            var seachCity = await SeachApi.SeachCityIdInApiAsync(city);

            var receiveDocument = await DocGenerator.CreateDoc(teams, headerSelect, routes, service, seachCity, _hostEnvironment.WebRootPath);

            downloadFile = $"{_hostEnvironment.WebRootPath}//files//{receiveDocument}";

            return View();
        }

        public FileContentResult Download()
        {
            var fileDownload = downloadFile.Split("//").ToList();
            var file = System.IO.File.ReadAllBytes(downloadFile);
            return File(file, "application/octet-stream", fileDownload.Last().ToString());
        }
    }
}
