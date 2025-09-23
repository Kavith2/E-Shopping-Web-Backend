using Backend1.Models.Entities;

namespace Backend1.IService
{
    public interface ICartService
    {
        Task<Cart> GetCartByUserId(string userId);
        Task AddToCart(string Id,string userId,string productName,int productPrice, string productId, int quantity);
        Task RemoveFromCart(string userId, string productId);
        Task ClearCart(string userId);
    }
}
