using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.MongoDb;

namespace Services
{
    public class DocGenerator
    {
        public static async Task<string> CreateDoc(List<Team> team, List<string> checkOption, List<List<string>> routes, string serviceSelect, City city, string webRoot)
        {
            var routesCount = routes.Count; //Conta a quantidade de rotas
            var allColumn = routes[0]; //Localiza colunas

            var serviceColumn = routes[0].FindIndex(column => column == "SERVIÇO" || column == "serviço"); //Localiza em qual coluna está o serviço
            var cityColumn = routes[0].FindIndex(column => column == "CIDADE" || column == "cidade"); //Localiza em qual coluna está a cidade
            var cepColumn = routes[0].FindIndex(column => column == "CEP" || column == "cep"); //Localiza em qual coluna está o cep

            for (int i = 0; i < routesCount; i++)
            {
                routes.Remove(routes.Find(route => route[cityColumn].ToUpper() != city.NameCity.ToUpper())); //Verifica as cidades que tem nomes diferentes da cidade selecionada e deleta elas
            }

            var divisionTeam = routes.Count / team.Count; //Divide as equipes pelo serviço
            var restDivision = routes.Count % team.Count; //Se sobra serviço realiza o resto da divisão para dividir eles entre as equipes

            var index = 0; //Contagem

            var pathFiles = $"{webRoot}//files"; //Cria uma pasta Files em wwwroot

            if (!Directory.Exists(pathFiles)) //Verifica se a pasta já existe
                Directory.CreateDirectory(pathFiles);

            var filename = $"Rota-{serviceSelect}-{DateTime.Now:dd-MM-yyyy}.docx"; //Local e nome do arquivo a ser salvo

            var CreateFile = $"{pathFiles}//{filename}"; //Criação do documento

            using (FileStream fileStream = new(CreateFile, FileMode.Create))
            {
                await using (StreamWriter writer = new(fileStream, Encoding.UTF8))
                {
                    writer.WriteLine($"{serviceSelect} - {DateTime.Now:dd/MM/yyyy}\t {city.NameCity}\n\n");

                    foreach (var item in team) //Escrita do arquivo localiza os times
                    {
                        writer.WriteLine("Time: " + item.NameTeam + "\nRotas:\n");

                        for (int i = 0; i < divisionTeam; i++)
                        {
                            if (i == 0 && restDivision > 0)
                                divisionTeam++;

                            if (i == 0)
                                restDivision--;

                            foreach (var check in checkOption)
                            {
                                writer.WriteLine($"{allColumn[int.Parse(check)]}: {routes[i + index][int.Parse(check)]}");
                            }

                            if ((i + 1) >= divisionTeam)
                            {
                                index += 1 + i;
                            }

                            writer.WriteLine("\n");
                        }

                        if (restDivision >= 0)
                            divisionTeam--;

                        writer.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                    }

                    writer.Close();
                }
                fileStream.Close();
            }
            return filename;
        }
    }
}
