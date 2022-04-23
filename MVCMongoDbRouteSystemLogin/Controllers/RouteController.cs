using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.MongoDb;
using OfficeOpenXml;
using Services;

namespace MVCMongoDbRouteSystemLogin.Controllers
{
    public class RouteController : Controller
    {
        public static List<List<string>> routes = new();
        public static List<string> headers = new();
        public static IEnumerable<string> serviceList;
        public static string service;
        public static string city;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeamOperationCity(IFormFile file)
        {
            //service = Request.Form["serviceName"].ToString();
            //city = Request.Form["cityName"].ToString();

            int cepColumn = 0;
            int serviceColumn = 0;
            bool check = false;
            List<string> header = new();
            List<string> listService = new();
            List<List<string>> content = new();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage fileexcel = new(file.OpenReadStream());
            ExcelWorksheet worksheet = fileexcel.Workbook.Worksheets.FirstOrDefault();

            var totalColumn = worksheet.Dimension.End.Column;
            var totalRow = worksheet.Dimension.End.Row;

            for (int column = 1; column < totalColumn; column++)
            {
                header.Add(worksheet.Cells[1, column].Value.ToString());

                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                    cepColumn = column - 1;

                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("SERVIÇO"))
                    serviceColumn = column;
            }

            for (int row = 2; row < totalRow; row++)
            {
                for (int line = serviceColumn; line <= serviceColumn; line++)
                {
                    if (worksheet.Cells[row, serviceColumn].Value?.ToString() != null)
                        listService.Add(worksheet.Cells[row, serviceColumn].Value?.ToString() ?? null);

                }
            }

            worksheet.Cells[2, 1, totalRow, totalColumn].Sort(cepColumn, false);

            for (int rows = 2; rows < totalRow; rows++)
            {
                List<string> contentLine = new();
                check = false;
                for (int columns = 1; columns < totalColumn; columns++)
                {
                    if (worksheet.Cells[rows, columns].Value?.ToString() != null)
                    {
                        var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                        contentLine.Add(conteudo);
                        check = true;
                    }
                }
                if (check)
                    content.Add(contentLine);
            }

            var removeRepeatService = listService;
            var listaSemDuplicidade = removeRepeatService.Distinct();
            headers = header;
            routes = content;
            serviceList = listaSemDuplicidade;
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

            var team = SeachApi.SeachTeamCityIdInApiAsync(city);

            ViewBag.TeamCity = await team;

            ViewBag.retornoReadFile = headers;
            return View();
        }

        [HttpPost]
        public async void GenereatorDoc()
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

            DocGenerator.CreateDoc(teams,  headerSelect, routes,  service, seachCity);

        }
    }
}
