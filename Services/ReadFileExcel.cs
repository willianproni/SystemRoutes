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
        /*       public static (List<List<string>>, List<string>) ReadFile(IFormFile fileXlsx, string service)
               {
                   int cepColumn = 0;
                   int serviceColumn = 0;
                   bool check = false;
                   List<string> header = new();
                   List<List<string>> content = new();

                   ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                   using ExcelPackage file = new(fileXlsx.OpenReadStream());
                   ExcelWorksheet worksheet = file.Workbook.Worksheets.FirstOrDefault();

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
                       for (int columns = 1; columns < totalColumn; columns++)
                       {
                           if (worksheet.Cells[rows, serviceColumn].Value?.ToString() == service)
                           {
                               var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                               contentLine.Add(conteudo);
                               check = true;
                           }
                           if (check)
                               content.Add(contentLine);
                       }
                   }

               }
           }*/

        /*    public (List<List<string>>, List<string>) ReadFileExcel(IFormFile excelFile)
            {
                int CepColumn = 0;
                List<string> header = new();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage pack = new(excelFile.OpenReadStream());
                ExcelWorksheet worksheet = pack.Workbook.Worksheets.FirstOrDefault();

                var totalColumn = worksheet.Dimension.End.Column;
                var totalRow = worksheet.Dimension.End.Row;

                for (int column = 1; column < totalColumn; column++)
                {
                    header.Add(worksheet.Cells[1, column].Value.ToString());

                    if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                        CepColumn = column - 1;
                }

                worksheet.Cells[2, 1, totalRow, totalColumn].Sort(CepColumn, false);
            }*/
    }
}
