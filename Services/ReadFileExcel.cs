using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Model;
using Model.MongoDb;
using OfficeOpenXml;

namespace Services
{
    public class ReadFileExcel
    {
        public static List<Route> ReadXls(IFormFile path)
        {
            var response = new List<Route>();

            //RotaService rotaService = new RotaService();

            //FileInfo ExistingFile = new FileInfo(@"C:\5by5\RouteSystem\rotasreadexcel.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage((Stream)path))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 1; row < 11; row++)
                {
                    var rota = new Route();
                    rota.OS = worksheet.Cells[row, 10].Value.ToString();
                    rota.Cidade = worksheet.Cells[row, 19].Value.ToString();
                    rota.Base = worksheet.Cells[row, 20].Value.ToString();
                    rota.Servico = worksheet.Cells[row, 23].Value.ToString();
                    rota.Endereco = worksheet.Cells[row, 27].Value.ToString();
                    rota.Numero = worksheet.Cells[row, 28].Value.ToString();
                    rota.Complemento = worksheet.Cells[row, 29].Value.ToString();
                    rota.Cep = worksheet.Cells[row, 30].Value.ToString();
                    rota.Bairro = worksheet.Cells[row, 32].Value.ToString();
                    //rotaService.Add(rota);
                    response.Add(rota);
                }

                var arquivo = new StreamWriter(@"C:\Test\excel.docx");

                arquivo.WriteLine("nome, emial");

                foreach (var item in response)
                {
                    var linha = $"OS:{item.OS}, Base:{item.Base}" +
                                $"\nCep: {item.Cep}" +
                                $"\nEndereço: {item.Endereco} Nº: {item.Numero}" +
                                $"\nBairro: {item.Bairro} Complemento: {item.Complemento}" +
                                $"\nServiço: {item.Servico}" +
                                $"\n------------------------------------------------------------\n";
                    arquivo.WriteLine(linha);
                }

                arquivo.Close();
            }
            return response;
        }
    }
}
