using Backend1.Data;
using Backend1.IService;
using Backend1.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static Backend1.Repositories.ProductService;

namespace Backend1.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(MongoDBContext context)
        {
            _products = context.Products;
        }


        public void AddProduct(List<Product> product)
        {
            _products.InsertMany(product);
        }


        public  async Task<Product?> UpdateProduct(string id,Product updatedProduct)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var update = Builders<Product>.Update
                .Set(p => p.Name, updatedProduct.Name)
                .Set(p => p.Description, updatedProduct.Description)
                .Set(p => p.Price, updatedProduct.Price)
                .Set(p => p.Image, updatedProduct.Image);

            var result = await _products.UpdateOneAsync(filter, update);    
            return result.ModifiedCount > 0 ? updatedProduct : null;
        }


        public Task<IActionResult> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }


        public List<Product> GetProductsByCategory(string category)
        {
            return _products.Find(p => p.Category == category).ToList();
        }


        public List<Product> GetAllProducts()
        {
            return _products.Find(p => true).ToList();
        }


        public async Task<Product?> GetById(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

       
    }


}

