using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend1.Models.Entities
{
    public class CartItem
    {
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int SubTotal { get; set; }
    }

    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }
}
