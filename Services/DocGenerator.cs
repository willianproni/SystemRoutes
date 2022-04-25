﻿using System;
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
        public static async void CreateDoc(List<Team> team, List<string> checkOption, List<List<string>> routes, string serviceSelect, City city)
        {
            var routesCount = routes.Count;
            var allColumn = routes[0];

            var serviceColumn = routes[0].FindIndex(column => column == "SERVIÇO" || column == "serviço");
            var cityColumn = routes[0].FindIndex(column => column == "CIDADE" || column == "cidade");
            var cepColumn = routes[0].FindIndex(column => column == "CEP" || column == "cep");

            for (int i = 0; i < routesCount; i++)
            {
                routes.Remove(routes.Find(route => route[cityColumn].ToUpper() != city.NameCity.ToUpper()));
                //routes.Remove(routes.Find(route => route[serviceColumn].ToUpper() != serviceSelect.ToUpper()));
            }

            var divisionTeam = routes.Count / team.Count;
            var restDivision = routes.Count % team.Count;

            var index = 0;

            var filename = $@"C:\Users\Willian Proni\Desktop\Rota-{serviceSelect}-{DateTime.Now:dd-MM-yyyy}.docx";

            using (FileStream fileStream = new(filename, FileMode.CreateNew))
            {
                await using (StreamWriter writer = new(fileStream, Encoding.UTF8))
                {
                    writer.WriteLine($"{serviceSelect} - {DateTime.Now:dd/MM/yyyy}\t {city.NameCity}\n\n");

                    foreach (var item in team)
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

                            writer.WriteLine("\t");
                        }

                        if (restDivision >= 0)
                            divisionTeam--;

                        writer.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                    }

                    writer.Close();
                }
                fileStream.Close();
            }
        }
    }
}
