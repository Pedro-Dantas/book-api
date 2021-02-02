using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioRestApi.Model
{
    public class Book
    {
        [BsonRepresentation(BsonType.ObjectId)]  
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Ano { get; set; }
        public string Paginas { get; set; }
        public List<string> Assunto { get; set; }
    }
}
