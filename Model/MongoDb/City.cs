using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Digite o nome da cidade.")]
        public string NameCity { get; set; }
        [Required(ErrorMessage = "Digite o nome do estado.")]
        public string State { get; set; }
    }
}
