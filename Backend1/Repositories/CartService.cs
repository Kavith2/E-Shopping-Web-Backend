using Backend1.Data;
using Backend1.IService;
using Backend1.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend1.Repositories
{
    public class CartService : ICartService
    {

        private readonly IMongoCollection<Cart> _cart;

        public CartService(MongoDBContext _context)
        {
            _cart = _context.Cart;
        }

        public async Task<Cart?> GetCartByUserId(string userId)
        {
            var cart = await _cart.Find(c => c.UserId == userId).FirstOrDefaultAsync();
            return cart ?? new Cart { UserId = userId, Items = new List<CartItem>() };
        }

        public async Task AddToCart(string Id, string userId, string productName, int productPrice, string productId, int quantity)
        {
            var cart = await GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ProductName=productName,
                    ProductPrice=productPrice,
                    SubTotal = 0
                });
            }

            await _cart.ReplaceOneAsync(c => c.UserId == userId, cart, new ReplaceOptions { IsUpsert = true });
        }


        public async Task RemoveFromCart(string userId, string productId)
        {
            var cart = await GetCartByUserId(userId);
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await _cart.ReplaceOneAsync(c => c.UserId == userId, cart);
        }

        public async Task ClearCart(string userId)
        {
            await _cart.DeleteOneAsync(c => c.UserId == userId);
        }

    }

}
