using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model.MongoDb
{
    public class Team
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string NameTeam { get; set; }
        public City City { get; set; }
        public List<Person> Persons { get; set; }
    }
}
