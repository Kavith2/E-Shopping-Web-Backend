using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend1.Models.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string? id { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public string? Email { get; set; }

    }
}
