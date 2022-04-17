using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MongoDb
{
    public class Team
    {
        public string Id { get; set; }
        public string NameTeam { get; set; }
        public City City { get; set; }
        public Person Person { get; set; }
    }
}
