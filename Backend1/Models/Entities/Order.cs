using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend1.Models.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // MongoDB ID

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("totalPrice")]
        public int TotalPrice { get; set; }

        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new();
    }

 


}
