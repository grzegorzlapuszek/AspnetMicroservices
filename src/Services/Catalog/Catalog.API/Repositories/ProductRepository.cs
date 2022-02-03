using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Find(p => true).ToListAsync();
            return products;
        }
        public async Task<Product> GetProduct(string id)
        {
            var product = await _context.Products.Find(p => p.Id == id).FirstAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var products = await _context.Products.Find(p => p.Name == name).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var products = await _context.Products.Find(p => p.Category == categoryName).ToListAsync();
            return products;
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: p=>p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }


        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(p => p.Id ==id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
