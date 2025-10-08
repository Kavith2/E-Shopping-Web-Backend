using Backend1.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend1.IService
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        void AddProduct(List<Product> product);
        Task<Product?> UpdateProduct(string id, Product updatedProduct);
        Task<IActionResult> DeleteProduct(string id);
        Task<Product?> GetById(string id);
        List<Product> GetProductsByCategory(string category);

    }
}
