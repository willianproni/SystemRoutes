using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Model;
using Model.MongoDb;
using OfficeOpenXml;

namespace Services
{
    public class ReadFileExcel
    {
        public static (List<string>, List<List<string>>, IEnumerable<string>) ReadFileInput(IFormFile file)
        {
            int cepColumn = 0;
            int serviceColumn = 0;
            bool check = false;
            List<string> header = new();
            List<string> listService = new();
            List<List<string>> content = new();
            try
            {

           
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage fileexcel = new(file.OpenReadStream());
            ExcelWorksheet worksheet = fileexcel.Workbook.Worksheets.FirstOrDefault();

            var totalColumn = worksheet.Dimension.End.Column;
            var totalRow = worksheet.Dimension.End.Row;

            for (int column = 1; column <= totalColumn; column++)
            {
                header.Add(worksheet.Cells[1, column].Value.ToString());

                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                    cepColumn = column - 1;

                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("SERVIÇO"))
                    serviceColumn = column;
            }

            for (int row = 2; row <= totalRow; row++)
            {
                for (int line = serviceColumn; line <= serviceColumn; line++)
                {
                    listService.Add(worksheet.Cells[row, serviceColumn].Value?.ToString() ?? null);
                }
            }

            worksheet.Cells[2, 1, totalRow, totalColumn].Sort(cepColumn, false);

            for (int rows = 1; rows < totalRow; rows++)
            {
                List<string> contentLine = new();
                check = false;
                for (int columns = 1; columns <= totalColumn; columns++)
                {
                    var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                    contentLine.Add(conteudo);
                    check = true;

                }
                if (check)
                    content.Add(contentLine);
            }

            var removeRepeatService = listService;
            var listaSemDuplicidade = removeRepeatService.Distinct();

            return (header,
            content,
            listaSemDuplicidade);
            }
            catch (System.IO.InvalidDataException)
            {

                return (null, null, null);
            }
        }
    }
}
