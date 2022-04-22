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
        public static string service;
        public static string city;
        public static IFormFile fileReceived;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeamOperationCity(IFormFile file)
        {
            service = Request.Form["serviceName"].ToString();
            //city = Request.Form["cityName"].ToString();

            int cepColumn = 0;
            int serviceColumn = 0;
            bool check = false;
            List<string> header = new();
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

            worksheet.Cells[2, 1, totalRow, totalColumn].Sort(cepColumn, false);

            for (int rows = 2; rows < totalRow; rows++)
            {
                List<string> contentLine = new();
                check = false;
                for (int columns = 1; columns < totalColumn; columns++)
                {
                    if (worksheet.Cells[rows, serviceColumn].Value?.ToString().ToUpper() == service)
                    {
                        var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                        contentLine.Add(conteudo);
                        check = true;
                    }
                }
                if (check)
                    content.Add(contentLine);
            }

            headers = header;
            routes = content;



            return RedirectToAction(nameof(SelectServiceAndCity));
        }

        public async Task<IActionResult> SelectServiceAndCity()
        {
            var localizarcity = SeachApi.GetAllCityInApi();
            ViewBag.AllCity = await localizarcity;
          
            return View();
        }

        public async Task<IActionResult> SelectHeader()
        {
            city = Request.Form["cityName"].ToString();

            var teams = await SeachApi.SeachTeamCityIdInApiAsync(city);

            ViewBag.TeamsAvailable = teams;

            ViewBag.retornoReadFile = headers;
            return View();
        }

        public void GenereatorDoc()
        {

        }

    }
}
