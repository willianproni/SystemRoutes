using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model.MongoDb
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name ="Nome")]
        [Required(ErrorMessage ="Digite o nome da pessoa.")]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
