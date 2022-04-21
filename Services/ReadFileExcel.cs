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
        /*        public static List<Route> ReadXls(IFormFile path)
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

                public static object ReadFileXls(IFormFile FileXls)
                {
                    List<string> list = new();
                    int cepColumn = 0;
                    IDictionary<string, List<String>> routes = new Dictionary<string, List<String>>();

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new(FileXls.OpenReadStream()))
                    {
                        ExcelWorksheet workbook = package.Workbook.Worksheets[0];
                        int totalColumns = workbook.Dimension.End.Column;
                        int totalrows = workbook.Dimension.End.Row;

                        for (int row = 1; row < totalrows; row++)
                        {
                            list = new List<string>();
                            for (int colums = 0; colums < totalColumns; colums++)
                            {
                                var conteudo = workbook.Cells[row, colums].Value == null ? "" : workbook.Cells[row, colums].Value;
                                if (conteudo == null && colums == totalColumns - 1)
                                    break;

                                if (conteudo.ToString().ToUpper() == "CEP")
                                    cepColumn = colums;
                                list.Add(conteudo.ToString());
                            }
                            if (workbook.Cells[row, cepColumn].Value == null || workbook.Cells[row, cepColumn].Value.ToString() == "")
                                break;

                            routes.Add(workbook.Cells[row, cepColumn].Value.ToString(), list);
                        }
                        var value = routes.OrderBy(Values => Values.Key).ToDictionary(values => values.Key, values => values.Key);
                        return value;
                    }
                }

                public static object ReadFileXlss()
                {
                    List<string> list = new();
                    int cepColumn = 0;
                    IDictionary<string, List<String>> routes = new Dictionary<string, List<String>>();

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var arquivoExcel = new ExcelPackage(new FileInfo(@"C:\5by5\RouteSystem\rotasreadexcel.xlsx"));

                    ExcelWorksheet workbook = arquivoExcel.Workbook.Worksheets[0];
                    int totalColumns = workbook.Dimension.End.Column;
                    int totalrows = workbook.Dimension.End.Row;

                    for (int row = 1; row < totalrows; row++)
                    {
                        list = new List<string>();
                        for (int colums = 1; colums < totalColumns; colums++)
                        {
                            var conteudo = workbook.Cells[row, colums].Value == null ? "" : workbook.Cells[row, colums].Value;
                            if (conteudo == null && colums == totalColumns - 1)
                                break;

                            if (conteudo.ToString().ToUpper() == "CEP")
                                cepColumn = colums;
                            list.Add(conteudo.ToString());
                            Console.WriteLine($"{conteudo}");
                        }
                        if (workbook.Cells[row, cepColumn].Value == null || workbook.Cells[row, cepColumn].Value.ToString() == "")
                            break;

                        routes.Add(workbook.Cells[row, cepColumn].Value.ToString(), list);
                    }
                    var value = routes.OrderBy(Values => Values.Key).ToDictionary(values => values.Key, values => values.Key);
                    return value;

                }*/

        public static List<string> ReadFile(IFormFile file)
        {

            //Data
            DataTable dt = new DataTable();

            var xls = new XLWorkbook((file.OpenReadStream()));
            var planilha = xls.Worksheets.First(w => w.Name == "Planilha1" || w.Name == "Sheet1");
            var totalLinhas = planilha.Rows().Count();

            //Get Columns
            List<String> columns = new List<string>();
            var qtdColumns = planilha.Columns().Count();
            for (int i = 1; i < qtdColumns; i++)
            {
                var col = planilha.Column(i).FirstCell().Value.ToString();
                dt.Columns.Add(col);
                columns.Add(col);
            }

            return columns;

/*            //Get Values
            var d = new string[columns.Count()];
            for (int l = 2; l <= totalLinhas; l++)
            {
                int index = 0;
                for (int i = 1; i <= columns.Count(); i++)
                {
                    d[index] = planilha.Column(i).Cell(l).Value.ToString();
                    index++;
                }
                dt.Rows.Add(d);
            }

            //Example Data Order by
            dt.DefaultView.Sort = "cep ASC";
            dt = dt.DefaultView.ToTable();

            //Print Data
            StringBuilder sbArquivo = new StringBuilder();

            for (int i = 1; i < dt.Rows.Count - 1; i++)
            {
                int j = 0;
                foreach (var item in columns)
                {
                    sbArquivo.Append(item + ": " + dt.Rows[i][j] + Environment.NewLine);
                    j++;
                }
            }
            Console.WriteLine(sbArquivo.ToString());*/
        }
    }
}
